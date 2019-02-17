using LDF.Structures;
using NUnit.Framework;

namespace Tests
{
    public class Array2DTests
    {
        [Test]
        public void Get()
        {
            const int width = 2;
            int[] array2D =
            {1, 2,
                3, 4};
            
            Assert.AreEqual(1,array2D.Get2D(0, 0, width));
            Assert.AreEqual(2,array2D.Get2D(1, 0, width));
            Assert.AreEqual(3,array2D.Get2D(0, 1, width));
            Assert.AreEqual(4,array2D.Get2D(1, 1, width));
        }
        
        [Test]
        public void Set()
        {
            const int width = 2;
            int[] array2D =
            {1, 2,
                3, 4};
            
            array2D.Set2D(0,0,width,111);
            array2D.Set2D(1,0,width,222);
            array2D.Set2D(0,1,width,333);
            array2D.Set2D(1,1,width,444);
            
            Assert.AreEqual(111,array2D.Get2D(0, 0, width));
            Assert.AreEqual(222,array2D.Get2D(1, 0, width));
            Assert.AreEqual(333,array2D.Get2D(0, 1, width));
            Assert.AreEqual(444,array2D.Get2D(1, 1, width));
        }
    }
}