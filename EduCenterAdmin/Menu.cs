using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduCenterAdmin
{
    abstract class Menu
    {
        protected List<Button> _buttons;
        protected SplitterPanel _buttonsPanel;
        protected FlowLayoutPanel _cardsPanel;
        protected EducationalCenterContext _context;

        protected Menu(List<Button> buttons, SplitterPanel buttonsPanel, FlowLayoutPanel cardsPanel, EducationalCenterContext context)
        {
            _buttons = buttons;
            _buttonsPanel = buttonsPanel;
            _cardsPanel = cardsPanel;
            _context = context;
        }

        public virtual void ShowMenu(Menu menu) { }
    }

    class MainMenu : Menu
    {
        StudentsMenu _studentMenu;
        TeachersMenu _teachersMenu;
        CoursesMenu _coursesMenu;
        ClassesMenu _classesMenu;
        RegistrationsMenu _registrationsMenu;

        public MainMenu(List<Button> buttons, SplitterPanel buttonsPanel, FlowLayoutPanel cardsPanel, EducationalCenterContext context, StudentsMenu studentMenu, TeachersMenu teachersMenu, CoursesMenu coursesMenu, ClassesMenu classesMenu, RegistrationsMenu registrationsMenu) : base(buttons, buttonsPanel, cardsPanel, context) 
        {
            _studentMenu = studentMenu;
            _teachersMenu = teachersMenu;
            _coursesMenu = coursesMenu;
            _classesMenu = classesMenu;
            _registrationsMenu = registrationsMenu;
        }

        public override void ShowMenu(Menu menu) 
        {
            _buttons.Clear();

            for(int i = 0; i < 5; i++)
            {
                _buttons.Add(new Button());
            }

            _buttonsPanel.Controls.Clear();

            _buttons[0].Text = "Работа с студетами";
            _buttons[1].Text = "Работа с преподователями";
            _buttons[2].Text = "Работа с курсами";
            _buttons[3].Text = "Работа с занятиями";
            _buttons[4].Text = "Работа с регистрациями";

            _buttons[0].Click += (sender, e) => { _studentMenu.ShowMenu(this); };
            _buttons[1].Click += (sender, e) => { _teachersMenu.ShowMenu(this); };
            _buttons[2].Click += (sender, e) => { _coursesMenu.ShowMenu(this); };
            _buttons[3].Click += (sender, e) => { _classesMenu.ShowMenu(this); };
            _buttons[4].Click += (sender, e) => { _registrationsMenu.ShowMenu(this); };

            for (int i = 0; i < 5; i++) 
            {
                _buttonsPanel.Controls.Add(_buttons[i]);
                _buttons[i].Location = new Point(10, i * 40 + 10);
                _buttons[i].Size = new Size(200, 30);
            }

            

        }
    }

    class StudentsMenu : Menu
    {
        public StudentsMenu(List<Button> buttons, SplitterPanel buttonsPanel, FlowLayoutPanel cardsPanel, EducationalCenterContext context) : base(buttons, buttonsPanel, cardsPanel, context)
        {
        }

        public override void ShowMenu(Menu menu)
        {
            _buttons.Clear();

            for(int i = 0; i < 4; i++)
            {
                _buttons.Add(new Button());
            }

            _buttonsPanel.Controls.Clear();

            _buttons[0].Text = "Показать студентов";
            _buttons[0].Click += (sender, e) => { ShowRecorsd(); };
            _buttons[1].Text = "Добавить студента";
            _buttons[1].Click += (sender, e) => { AddRecord(); };
            _buttons[2].Text = "Удалить студента";
            _buttons[2].Click += (sender, e) => { DeleteRecord(); };
            _buttons[3].Text = "Главное меню";
            _buttons[3].Click += (sender, e) => { menu.ShowMenu(this); };

            for (int i = 0; i < 4; i++) 
            {
                _buttonsPanel.Controls.Add(_buttons[i]);
                _buttons[i].Location = new Point(10, i * 40 + 10);
                _buttons[i].Size = new Size(200, 30);
               
            }
        }

        private void ShowRecorsd()
        {
            _context = new EducationalCenterContext();

            _cardsPanel.Controls.Clear();

            var Students = _context.Students.ToList();
            for (int i = 0; i < Students.Count; i++)
            {
                StudentCard card = new StudentCard(Students[i], _cardsPanel);
                card.DisplayCard();
            }
        }

        private void AddRecord()
        {
            _context = new EducationalCenterContext();

            Form form = new Form();

            List<TextBox> textBoxes = [];

            for (int i = 0; i < 5; i++)
            {
                textBoxes.Add(new TextBox());
            }

            textBoxes[0].Text = "Введите имя";
            textBoxes[1].Text = "Введите фамилию";
            textBoxes[2].Text = "Введите адресс";
            textBoxes[3].Text = "Введите емейл";
            textBoxes[4].Text = "Введите телефон";

            for (int i = 0; i < 5; i++)
            {
                form.Controls.Add(textBoxes[i]);
                textBoxes[i].Location = new Point(10, i * 40 + 10);
                textBoxes[i].Size = new Size(200, 30);
            }

            Button button = new Button();
            form.Controls.Add(button);
            button.Text = "Добавить студента";
            button.Location = new Point(10, 220);

            button.Click += (sender, e) => 
            {
                if (!string.IsNullOrEmpty(textBoxes[0].Text) 
                    && !string.IsNullOrEmpty(textBoxes[1].Text) 
                    && !string.IsNullOrEmpty(textBoxes[2].Text) 
                    && !string.IsNullOrEmpty(textBoxes[3].Text) 
                    && !string.IsNullOrEmpty(textBoxes[4].Text))
                {
                    Student student = new Student() 
                    {
                        FirstName = textBoxes[0].Text,
                        LastName = textBoxes[1].Text,
                        Adress = textBoxes[2].Text,
                        Email = textBoxes[3].Text,
                        Phone = textBoxes[4].Text,
                    };

                    _context.Students.Add(student);
                    _context.SaveChanges();
                    form.Close();
                    ShowRecorsd();
                }
                else
                {
                    MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            form.ShowDialog();
        }

        private void DeleteRecord()
        {
            _context = new EducationalCenterContext();

            Form form = new Form();

            List<TextBox> textBoxes = [];

            for (int i = 0; i < 1; i++)
            {
                textBoxes.Add(new TextBox());
            }

            textBoxes[0].Text = "Введите идентификатор";

            for (int i = 0; i < 1; i++)
            {
                form.Controls.Add(textBoxes[i]);
                textBoxes[i].Location = new Point(10, i * 40 + 10);
                textBoxes[i].Size = new Size(200, 30);
            }

            Button button = new Button();
            form.Controls.Add(button);
            button.Text = "Удалить студента";
            button.Location = new Point(10, 220);

            button.Click += (sender, e) =>
            {
                if (int.TryParse(textBoxes[0].Text, out int IdValue))
                {
                    Student student = _context.Students.Where(s => s.StudentId == IdValue).FirstOrDefault();

                    if(student != null)
                    {
                        _context.Students.Remove(student);
                        _context.SaveChanges();
                        form.Close();
                        ShowRecorsd();
                    }
                    else
                    {
                        MessageBox.Show("Такого преподавателя не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            form.ShowDialog();
        }
    }

    class TeachersMenu : Menu
    {
        public TeachersMenu(List<Button> buttons, SplitterPanel buttonsPanel, FlowLayoutPanel cardsPanel, EducationalCenterContext context) : base(buttons, buttonsPanel, cardsPanel, context)
        {
        }

        public override void ShowMenu(Menu menu)
        {
            _buttons.Clear();

            for (int i = 0; i < 4; i++)
            {
                _buttons.Add(new Button());
            }

            _buttonsPanel.Controls.Clear();

            _buttons[0].Text = "Показать преподавателей";
            _buttons[0].Click += (sender, e) => { ShowRocords(); };
            _buttons[1].Text = "Добавить преподавателя";
            _buttons[1].Click += (sender, e) => { AddRecord(); };
            _buttons[2].Text = "Удалить преподавателя";
            _buttons[2].Click += (sender, e) => { DeleteRecord(); };
            _buttons[3].Text = "Главное меню";
            _buttons[3].Click += (sender, e) => { menu.ShowMenu(this); };

            for (int i = 0; i < 4; i++)
            {
                _buttonsPanel.Controls.Add(_buttons[i]);
                _buttons[i].Location = new Point(10, i * 40 + 10);
                _buttons[i].Size = new Size(200, 30);

            }
        }

        private void ShowRocords()
        {
            _context = new EducationalCenterContext();

            _cardsPanel.Controls.Clear();

            var Teachers = _context.Teachers.ToList();
            for (int i = 0; i < Teachers.Count; i++)
            {
                TeacherCard card = new TeacherCard(Teachers[i], _cardsPanel);
                card.DisplayCard();
            }
        }

        private void AddRecord()
        {
            _context = new EducationalCenterContext();

            Form form = new Form();

            List<TextBox> textBoxes = [];

            for (int i = 0; i < 5; i++)
            {
                textBoxes.Add(new TextBox());
            }

            textBoxes[0].Text = "Введите имя";
            textBoxes[1].Text = "Введите фамилию";
            textBoxes[2].Text = "Введите специализацию";
            textBoxes[3].Text = "Введите емейл";
            textBoxes[4].Text = "Введите телефон";

            for (int i = 0; i < 5; i++)
            {
                form.Controls.Add(textBoxes[i]);
                textBoxes[i].Location = new Point(10, i * 40 + 10);
                textBoxes[i].Size = new Size(200, 30);
            }

            Button button = new Button();
            form.Controls.Add(button);
            button.Text = "Добавить преподавателя";
            button.Location = new Point(10, 220);

            button.Click += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(textBoxes[0].Text)
                    && !string.IsNullOrEmpty(textBoxes[1].Text)
                    && !string.IsNullOrEmpty(textBoxes[2].Text)
                    && !string.IsNullOrEmpty(textBoxes[3].Text)
                    && !string.IsNullOrEmpty(textBoxes[3].Text))
                {
                    Teacher teacher = new Teacher()
                    {
                        FirstName = textBoxes[0].Text,
                        LastName = textBoxes[1].Text,
                        Specialisation = textBoxes[2].Text,
                        Email = textBoxes[3].Text,
                        Phone = textBoxes[4].Text
                    };

                    _context.Teachers.Add(teacher);
                    _context.SaveChanges();
                    form.Close();
                    ShowRocords();
                }
                else
                {
                    MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            form.ShowDialog();
        }

        private void DeleteRecord()
        {
            _context = new EducationalCenterContext();

            Form form = new Form();

            List<TextBox> textBoxes = [];

            for (int i = 0; i < 1; i++)
            {
                textBoxes.Add(new TextBox());
            }

            textBoxes[0].Text = "Введите идентификатор";

            for (int i = 0; i < 1; i++)
            {
                form.Controls.Add(textBoxes[i]);
                textBoxes[i].Location = new Point(10, i * 40 + 10);
                textBoxes[i].Size = new Size(200, 30);
            }

            Button button = new Button();
            form.Controls.Add(button);
            button.Text = "Удалить преподавателя";
            button.Location = new Point(10, 220);

            button.Click += (sender, e) =>
            {
                if (int.TryParse(textBoxes[0].Text, out int IdValue))
                {
                    Teacher teacher = _context.Teachers.Where(s => s.TeacherId == IdValue).FirstOrDefault();

                    if (teacher != null)
                    {
                        _context.Teachers.Remove(teacher);
                        _context.SaveChanges();
                        form.Close();
                        ShowRocords();
                    }
                    else
                    {
                        MessageBox.Show("Такого преподаваьеля не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            form.ShowDialog();
        }
    }

    class CoursesMenu : Menu
    {
        public CoursesMenu(List<Button> buttons, SplitterPanel buttonsPanel, FlowLayoutPanel cardsPanel, EducationalCenterContext context) : base(buttons, buttonsPanel, cardsPanel, context)
        {
        }

        public override void ShowMenu(Menu menu)
        {
            _buttons.Clear();

            for (int i = 0; i < 4; i++)
            {
                _buttons.Add(new Button());
            }

            _buttonsPanel.Controls.Clear();

            _buttons[0].Text = "Показать курсы";
            _buttons[0].Click += (sender, e) => { ShowRecords(); };
            _buttons[1].Text = "Добавить курс";
            _buttons[1].Click += (sender, e) => { AddRecord(); };
            _buttons[2].Text = "Удалить курс";
            _buttons[2].Click += (sender, e) => { DeleteRecord(); };
            _buttons[3].Text = "Главное меню";
            _buttons[3].Click += (sender, e) => { menu.ShowMenu(this); };

            for (int i = 0; i < 4; i++)
            {
                _buttonsPanel.Controls.Add(_buttons[i]);
                _buttons[i].Location = new Point(10, i * 40 + 10);
                _buttons[i].Size = new Size(200, 30);

            }
        }

        private void ShowRecords()
        {
            _context = new EducationalCenterContext();

            _cardsPanel.Controls.Clear();

            var Courses = _context.Courses.ToList();
            for (int i = 0; i < Courses.Count; i++)
            {
                CourseCard card = new CourseCard(Courses[i], _cardsPanel);
                card.DisplayCard();
            }
        }

        private void AddRecord()
        {
            _context = new EducationalCenterContext();

            Form form = new Form();

            List<TextBox> textBoxes = [];

            for (int i = 0; i < 4; i++)
            {
                textBoxes.Add(new TextBox());
            }

            textBoxes[0].Text = "Введите название курса";
            textBoxes[1].Text = "Введите описание курса";
            textBoxes[2].Text = "Введите продолжительность";
            textBoxes[3].Text = "Введите цену";

            for (int i = 0; i < 4; i++)
            {
                form.Controls.Add(textBoxes[i]);
                textBoxes[i].Location = new Point(10, i * 40 + 10);
                textBoxes[i].Size = new Size(200, 30);
            }

            Button button = new Button();
            form.Controls.Add(button);
            button.Text = "Добавить курс";
            button.Location = new Point(10, 220);

            button.Click += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(textBoxes[0].Text)
                    && !string.IsNullOrEmpty(textBoxes[1].Text)
                    && !string.IsNullOrEmpty(textBoxes[2].Text)
                    && !string.IsNullOrEmpty(textBoxes[3].Text))
                {
                    Course course = new Course()
                    {
                        CourseName = textBoxes[0].Text,
                        Description = textBoxes[1].Text,
                        Duration = Convert.ToInt32(textBoxes[2].Text),
                        Price = Convert.ToInt32(textBoxes[3].Text)
                    };

                    _context.Courses.Add(course);
                    _context.SaveChanges();
                    form.Close();
                    ShowRecords();
                }
                else
                {
                    MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            form.ShowDialog();
        }

        private void DeleteRecord()
        {
            _context = new EducationalCenterContext();

            Form form = new Form();

            List<TextBox> textBoxes = [];

            for (int i = 0; i < 1; i++)
            {
                textBoxes.Add(new TextBox());
            }

            textBoxes[0].Text = "Введите идентификатор";

            for (int i = 0; i < 1; i++)
            {
                form.Controls.Add(textBoxes[i]);
                textBoxes[i].Location = new Point(10, i * 40 + 10);
                textBoxes[i].Size = new Size(200, 30);
            }

            Button button = new Button();
            form.Controls.Add(button);
            button.Text = "Удалить курс";
            button.Location = new Point(10, 220);

            button.Click += (sender, e) =>
            {
                if (int.TryParse(textBoxes[0].Text, out int IdValue))
                {
                    Course course = _context.Courses.Where(s => s.CourseId == IdValue).FirstOrDefault();

                    if (course != null)
                    {
                        _context.Courses.Remove(course);
                        _context.SaveChanges();
                        form.Close();
                        ShowRecords();
                    }
                    else
                    {
                        MessageBox.Show("Такого курса не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            form.ShowDialog();
        }
    }

    class ClassesMenu : Menu
    {
        public ClassesMenu(List<Button> buttons, SplitterPanel buttonsPanel, FlowLayoutPanel cardsPanel, EducationalCenterContext context) : base(buttons, buttonsPanel, cardsPanel, context)
        {
        }

        public override void ShowMenu(Menu menu)
        {
            _buttons.Clear();

            for (int i = 0; i < 4; i++)
            {
                _buttons.Add(new Button());
            }

            _buttonsPanel.Controls.Clear();

            _buttons[0].Text = "Показать занятия";
            _buttons[0].Click += (sender, e) => { ShowRecords(); };
            _buttons[1].Text = "Добавить занятие";
            _buttons[1].Click += (sender, e) => { AddRecord(); };
            _buttons[2].Text = "Удалить занятие";
            _buttons[2].Click += (sender, e) => { DeleteRecord(); };
            _buttons[3].Text = "Главное меню";
            _buttons[3].Click += (sender, e) => { menu.ShowMenu(this); };

            for (int i = 0; i < 4; i++)
            {
                _buttonsPanel.Controls.Add(_buttons[i]);
                _buttons[i].Location = new Point(10, i * 40 + 10);
                _buttons[i].Size = new Size(200, 30);

            }
        }

        private void ShowRecords()
        {
            _context = new EducationalCenterContext();

            _cardsPanel.Controls.Clear();

            var Classes = _context.Classes.Include(c => c.Course).Include(c => c.Teacher).ToList();
            for (int i = 0; i < Classes.Count; i++)
            {
                ClassCard card = new ClassCard(Classes[i], _cardsPanel, _context);
                card.DisplayCard();
            }
        }

        private void AddRecord()
        {
            _context = new EducationalCenterContext();

            var Courses = _context.Courses.ToList();
            var Teachers = _context.Teachers.ToList();

            Form form = new Form() { Size = new Size(600, 500), Text = "Создание заннятия"};

            ComboBox courses = new ComboBox() {Size = new Size(500, 25), Location = new Point(10, 10)};
            ComboBox teachers = new ComboBox() { Size = new Size(500, 25), Location = new Point(10, 40) };
            DateTimePicker startDate = new DateTimePicker() { Size = new Size(500, 25), Location = new Point(10, 70) };
            DateTimePicker endDate = new DateTimePicker() { Size = new Size(500, 25), Location = new Point(10, 100) };
            TextBox durationTB = new TextBox() { Size = new Size(500, 25), Location = new Point(10, 130) };

            var coursesDict = new Dictionary<int, string>();
            var teachersDict = new Dictionary<int, string>();

            coursesDict.Clear();
            teachersDict.Clear();

            foreach(Course course in Courses)
            {
                coursesDict.Add(course.CourseId, course.CourseName + ", " + course.Description);
            }

            foreach(Teacher teacher in Teachers)
            {
                teachersDict.Add(teacher.TeacherId, teacher.FirstName + ", " + teacher.LastName + ", " + teacher.Specialisation);
            }

            courses.Items.Clear();
            teachers.Items.Clear();

            courses.DataSource = coursesDict.ToList();
            courses.DisplayMember = "Value";
            courses.ValueMember = "Key";

            teachers.DataSource = teachersDict.ToList();
            teachers.DisplayMember = "Value"; 
            teachers.ValueMember = "Key";
            
            
            form.Controls.Add(courses);
            form.Controls.Add(teachers);
            form.Controls.Add(startDate);
            form.Controls.Add(endDate);
            form.Controls.Add(durationTB);

            Button button = new Button();
            form.Controls.Add(button);
            button.Text = "Добавить занятие";
            button.Location = new Point(10, 220);

            button.Click += (sender, e) =>
            {
                int teacherId = int.Parse(teachers.SelectedValue.ToString());
                int courseId = int.Parse(courses.SelectedValue.ToString());

                Teacher teacher = _context.Teachers.Where(x => x.TeacherId == teacherId).FirstOrDefault();
                Course courese = _context.Courses.Where(x => x.CourseId == courseId).FirstOrDefault();

                Class clas = new Class() { Course = courese, Teacher = teacher, StartDate = startDate.Value, EndDate = endDate.Value, Duration =  Convert.ToInt32(durationTB.Text)};

                _context.Classes.Add(clas);
                _context.SaveChanges();

                form.Close();

                ShowRecords();
            };

            form.ShowDialog();
        }

        private void DeleteRecord()
        {
            _context = new EducationalCenterContext();

            Form form = new Form();

            List<TextBox> textBoxes = [];

            for (int i = 0; i < 1; i++)
            {
                textBoxes.Add(new TextBox());
            }

            textBoxes[0].Text = "Введите идентификатор";

            for (int i = 0; i < 1; i++)
            {
                form.Controls.Add(textBoxes[i]);
                textBoxes[i].Location = new Point(10, i * 40 + 10);
                textBoxes[i].Size = new Size(200, 30);
            }

            Button button = new Button();
            form.Controls.Add(button);
            button.Text = "Удалить занятие";
            button.Location = new Point(10, 220);

            button.Click += (sender, e) =>
            {
                if (int.TryParse(textBoxes[0].Text, out int IdValue))
                {
                    Class clas = _context.Classes.Where(s => s.ClassId == IdValue).FirstOrDefault();

                    if (clas != null)
                    {
                        _context.Classes.Remove(clas);
                        _context.SaveChanges();
                        form.Close();
                        ShowRecords();
                    }
                    else
                    {
                        MessageBox.Show("Такого занятия не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            form.ShowDialog();
        }
    }

    class RegistrationsMenu : Menu
    {
        public RegistrationsMenu(List<Button> buttons, SplitterPanel buttonsPanel, FlowLayoutPanel cardsPanel, EducationalCenterContext context) : base(buttons, buttonsPanel, cardsPanel, context)
        {
        }

        public override void ShowMenu(Menu menu)
        {
            _buttons.Clear();

            for (int i = 0; i < 4; i++)
            {
                _buttons.Add(new Button());
            }

            _buttonsPanel.Controls.Clear();

            _buttons[0].Text = "Показать регистрации";
            _buttons[0].Click += (sender, e) => { ShowRecords(); };
            _buttons[1].Text = "Добавить регистрацию";
            _buttons[1].Click += (sender, e) => { AddRecord(); };
            _buttons[2].Text = "Удалить регистрацию";
            _buttons[2].Click += (sender, e) => { DeleteRecord(); };
            _buttons[3].Text = "Главное меню";
            _buttons[3].Click += (sender, e) => { menu.ShowMenu(this); };

            for (int i = 0; i < 4; i++)
            {
                _buttonsPanel.Controls.Add(_buttons[i]);
                _buttons[i].Location = new Point(10, i * 40 + 10);
                _buttons[i].Size = new Size(200, 30);

            }
        }

        private void ShowRecords()
        {
            _context = new EducationalCenterContext();

            _cardsPanel.Controls.Clear();

            var Registrations = _context.Registrations.Include(r => r.Class).Include(r => r.Student).ToList();
            for (int i = 0; i < Registrations.Count; i++)
            {
                RegistrationCard card = new RegistrationCard(Registrations[i], _cardsPanel, _context);
                card.DisplayCard();
            }
        }

        private void AddRecord()
        {
            _context = new EducationalCenterContext();

            var Classes = _context.Classes.Include(c => c.Course).Include(c => c.Teacher).ToList();
            var Students = _context.Students.ToList();

            Form form = new Form() { Size = new Size(600, 500), Text = "Создание регистрацию" };

            ComboBox classes = new ComboBox() { Size = new Size(500, 25), Location = new Point(10, 10) };
            ComboBox students = new ComboBox() { Size = new Size(500, 25), Location = new Point(10, 40) };
            DateTimePicker registrationDate = new DateTimePicker() { Size = new Size(500, 25), Location = new Point(10, 70) };
            TextBox marknTB = new TextBox() { Size = new Size(500, 25), Location = new Point(10, 130) };

            var classesDict = new Dictionary<int, string>();
            var studentsDict = new Dictionary<int, string>();

            classesDict.Clear();
            studentsDict.Clear();

            foreach (Class clas in Classes)
            {
                classesDict.Add(clas.ClassId, clas.Teacher.FirstName + ", " + clas.Course.CourseName);
            }

            foreach (Student student in Students)
            {
                studentsDict.Add(student.StudentId, student.FirstName + ", " + student.LastName);
            }

            classes.Items.Clear();
            students.Items.Clear();

            classes.DataSource = classesDict.ToList();
            classes.DisplayMember = "Value";
            classes.ValueMember = "Key";

            students.DataSource = studentsDict.ToList();
            students.DisplayMember = "Value";
            students.ValueMember = "Key";


            form.Controls.Add(classes);
            form.Controls.Add(students);
            form.Controls.Add(registrationDate);
            form.Controls.Add(marknTB);

            Button button = new Button();
            form.Controls.Add(button);
            button.Text = "Добавить регистрацию";
            button.Location = new Point(10, 220);

            button.Click += (sender, e) =>
            {
                int classId = int.Parse(classes.SelectedValue.ToString());
                int studentId = int.Parse(students.SelectedValue.ToString());

                Class clas = _context.Classes.Where(x => x.ClassId == classId).FirstOrDefault();
                Student student = _context.Students.Where(x => x.StudentId == studentId).FirstOrDefault();

                Registration registration = new Registration() { Class = clas, Student = student, RegistrationDate = registrationDate.Value, Mark = Convert.ToInt32(marknTB.Text) };

                _context.Registrations.Add(registration);
                _context.SaveChanges();

                form.Close();

                ShowRecords();
            };

            form.ShowDialog();
        }

        private void DeleteRecord()
        {
            _context = new EducationalCenterContext();

            Form form = new Form();

            List<TextBox> textBoxes = [];

            for (int i = 0; i < 1; i++)
            {
                textBoxes.Add(new TextBox());
            }

            textBoxes[0].Text = "Введите идентификатор";

            for (int i = 0; i < 1; i++)
            {
                form.Controls.Add(textBoxes[i]);
                textBoxes[i].Location = new Point(10, i * 40 + 10);
                textBoxes[i].Size = new Size(200, 30);
            }

            Button button = new Button();
            form.Controls.Add(button);
            button.Text = "Удалить рнгистрацию";
            button.Location = new Point(10, 220);

            button.Click += (sender, e) =>
            {
                if (int.TryParse(textBoxes[0].Text, out int IdValue))
                {
                    Registration registration = _context.Registrations.Where(s => s.RegistrationId == IdValue).FirstOrDefault();

                    if (registration != null)
                    {
                        _context.Registrations.Remove(registration);
                        _context.SaveChanges();
                        form.Close();
                        ShowRecords();
                    }
                    else
                    {
                        MessageBox.Show("Такой регистрации не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            form.ShowDialog();
        }
    }
}
