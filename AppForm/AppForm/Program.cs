using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace SortScores
{

    /// <summary>
    /// Class SortFileContent which reads content from file, sorts it and writes it into new file
    /// </summary>
    public class GradeScores
    {
        public string inputTextFile = null;
        public string inputTextFileName = null;
        public string inputTextFileLocation = null;

        /***********************************************************************************/
        /// <summary>
        /// Check if the file exists
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /**********************************************************************************/
        public bool checkIfFileExists( string filePath )
        {
            bool result = true;
            inputTextFile = Path.GetFullPath( filePath );
            var fileInfo = new FileInfo( inputTextFile );

            //Check if the file exists or if it is empty
            if ( !fileInfo.Exists )
            {
                Console.WriteLine( "File name does not exist" );
                result = false;
            }
            return result;
        }

        /**********************************************************************************/
        /// <summary>
        /// Check if the file has some data
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /**********************************************************************************/
        public bool checkIfFileEmpty( string filePath )
        {
            bool result = true;
            inputTextFile = Path.GetFullPath( filePath );
            var fileInfo = new FileInfo( inputTextFile );

            //Check if the file exists or if it is empty
            if ( 0 == fileInfo.Length )
            {
                Console.WriteLine( "File is empty" );
                result = false;
            }
            return result;
        }

        /**********************************************************************************/
        /// <summary>
        /// Method to set input file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /**********************************************************************************/
        public void setInputFilePath( string filePath )
        {
            inputTextFile = Path.GetFullPath( filePath );
            inputTextFileName = Path.GetFileNameWithoutExtension( filePath );
            inputTextFileLocation = Path.GetDirectoryName( filePath );
        }

        /**********************************************************************************/
        /// <summary>
        /// Method to check if the file is in correct format
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        /**********************************************************************************/
        public bool checkTextFormat()
        {
            bool result = false;

            foreach ( string line in File.ReadAllLines( inputTextFile ) )
            {
                if ( ( Regex.IsMatch( line, "[a-zA-Z]+[, ]+[a-zA-Z]+[, ]+[0-9]+" ) ) )
                {
                    result = true;
                }
                else
                {
                    Console.WriteLine( "Invalid data in file" );
                    break;
                }
            }
            return result;
        }

        /**********************************************************************************/
        /// <summary>
        /// Method to sort the names and grades
        /// </summary>
        /// <param name="sortResult"></param>
        /// <returns></returns>
        /**********************************************************************************/
        public IEnumerable<string> sortFileContent()
        {
            IEnumerable<string> sortedNames = null;
            sortedNames = File.ReadLines( inputTextFile )
                                          .Select( entry => entry.Split( ',' ).ToArray())
                                          .OrderByDescending( entry => entry[2] )
                                          .ThenByDescending( entry => entry[1] )
                                          .ThenByDescending( entry => entry[0] )
                                          .Select( entry => string.Join( ",", entry ) );
            return sortedNames;
        }

        /**********************************************************************************/
        /// <summary>
        /// Method to write the final sorted data to output file
        /// </summary>
        /// <param name="sortedString"></param>
        /**********************************************************************************/
        public void writeOutputFileData( IEnumerable<string> sortedString )
        {
            //Create a new file name with name as required
            string outputTextFile = inputTextFileLocation + inputTextFileName + "-graded" +
                                                            Path.GetExtension( inputTextFile );
            string displayFileName = inputTextFileName + "-graded" + Path.GetExtension( inputTextFile );

            //Write sorted data to the new file with the name as above
            foreach ( var row in sortedString)
            {
                Console.WriteLine( row );
            }
            File.WriteAllLines( outputTextFile, sortedString);
            Console.WriteLine( "Finished: created {0}", displayFileName );
        }

    }


    /**********************************************************************************/
    /// <summary>
    /// Main function
    /// </summary>
    /// <param name="args"></param>
    /**********************************************************************************/
    class Program
    {
        static void Main( string[] args )
        {
            if (0 < args.Length)
            {
                string userInput = args[0];
                IEnumerable<string> sortedString;
                GradeScores sortNames = new GradeScores();
                if( true == sortNames.checkIfFileExists( userInput ) )
                {
                    if( true == sortNames.checkIfFileEmpty( userInput ) )
                    {
                        sortNames.setInputFilePath( userInput );
                        if( true == sortNames.checkTextFormat() )
                        {
                            sortedString = sortNames.sortFileContent();
                            sortNames.writeOutputFileData( sortedString );
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Main function: No arguments passed");
            }
        }
    }
}