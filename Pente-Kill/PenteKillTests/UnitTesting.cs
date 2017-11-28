using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PenteKillTests
{
    [TestClass]
    public class UnitTesting
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestBlackTilePlacement(){
            //will test if the black tile can be placed in the middle of the board in 4 spots
        }
        public void TestWhiteTilePlacement(){
            //will test if the white tile can be placed on any part of the board 
        }
        public void TestSave(){
            //will test if the saved game will load in the state it was saved
        }
        public void TestLoad(){
            //will test if game will load
        }
        public void TestBoardSize8(){
            //sets the board size to 8
        }

        public void TestBoardSize40(){
            //sets the board size to 40
        }
    }
}
