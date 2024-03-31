using Microsoft.EntityFrameworkCore;

namespace EduCenterAdmin
{
    public partial class Form1 : Form
    {
        EducationalCenterContext db;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StudentsMenu studentMenu = new ([], splitContainer1.Panel1, flowLayoutPanel1, db);
            TeachersMenu teachersMenu = new ([], splitContainer1.Panel1, flowLayoutPanel1, db);
            CoursesMenu coursesMenu = new ([], splitContainer1.Panel1, flowLayoutPanel1, db);
            ClassesMenu classesMenu = new([], splitContainer1.Panel1, flowLayoutPanel1, db);
            RegistrationsMenu registratiomnsMenu = new RegistrationsMenu([], splitContainer1.Panel1, flowLayoutPanel1, db);

            MainMenu mainMenu = new ([], splitContainer1.Panel1, flowLayoutPanel1, db, 
                studentMenu,
                teachersMenu,
                coursesMenu,
                classesMenu,
                registratiomnsMenu);

            mainMenu.ShowMenu(mainMenu);
        }  
    }
}
