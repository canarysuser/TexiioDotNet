using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Student
    {
        #region Encapsulated Fields
        int _rollNo; 
        private string _name=string.Empty;
        private string _gradeName=string.Empty;
        #endregion

        #region Properties 
        public int RollNo
        {
            get { return _rollNo; }
            set { _rollNo = value; } 
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        //Shortcut - Property Expressions 
        public string Grade { get => _gradeName; set => _gradeName = value; }

        //Compiler adds the backing field - can be used in training envs, not production envs. 
        public int TotalMarks { get; set; } //compiler completes the property declaration

        #endregion 

        #region 
        public Student() { _rollNo = 0; _name = "Not Applicable"; _gradeName = "Not Applicable"; }
        #endregion 

        //Methods 
        public void PrintDetails()
        {
            Console.WriteLine($"Roll No:{_rollNo}, Name:{_name}, Grade:{_gradeName}");
        }
    }
    
}
