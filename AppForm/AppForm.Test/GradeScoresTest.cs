using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppForm.Test
{
    [TestClass]
    public class GradeScoresTest
    {
        [TestMethod]
        public void testCheckIfFileExists()
        {
            SortScores.GradeScores checkFile = new SortScores.GradeScores();
            string fileName = "F:\\test.txt";
            bool retResult = checkFile.checkIfFileExists(fileName);
            Assert.IsTrue(retResult.Equals(true));

        }

        [TestMethod]
        public void testCheckIfFileEmpty()
        {
            SortScores.GradeScores checkFile = new SortScores.GradeScores();
            string fileName = "F:\\test.txt";
            bool retResult = checkFile.checkIfFileEmpty(fileName);
            Assert.IsTrue(retResult.Equals(true));
        }

        [TestMethod]
        public void testSetInputFilePath()
        {
            //Test passes if the user gives a valid file name. If we give an invalid
            //file path here, the unit test will automatically fail because of the 
            //exception that will be thrown and the test will automatically fail.

            SortScores.GradeScores checkFile = new SortScores.GradeScores();
            string fileName = "F:\\test.txt";
            checkFile.setInputFilePath(fileName);
        }

        [TestMethod]
        public void testCheckTextFormat()
        {
            SortScores.GradeScores checkFile = new SortScores.GradeScores();
            string fileName = "F:\\test.txt";
            checkFile.inputTextFile = Path.GetFullPath( fileName );
            bool retResult = checkFile.checkTextFormat();
            Assert.IsTrue(retResult.Equals(true));
        }

        [TestMethod]
        public void testSortFileContent()
        {
            SortScores.GradeScores checkFile = new SortScores.GradeScores();
            string fileName = "F:\\test.txt";
            checkFile.inputTextFile = Path.GetFullPath(fileName);
            Assert.IsNotNull(checkFile.sortFileContent());
        }

        [TestMethod]
        public void testWriteOutputFileData()
        {
            //Test passes if the user gives a valid file name. If we give an invalid
            //file path here, the unit test will automatically fail because of the 
            //exception that will be thrown and the test will automatically fail.

            SortScores.GradeScores checkFile = new SortScores.GradeScores();
            string fileName = "F:\\test.txt";
            checkFile.inputTextFile = Path.GetFullPath(fileName);
            IEnumerable<string> sortedString = checkFile.sortFileContent();
            checkFile.writeOutputFileData(sortedString);

        }
    }
}