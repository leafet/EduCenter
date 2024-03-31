using Microsoft.EntityFrameworkCore;

namespace EduCenterAdmin
{
    internal class StudentCard
    {
        private Student _student;
        private FlowLayoutPanel _parrent;

        public StudentCard(Student student, FlowLayoutPanel parrent)
        {
            _student = student;
            _parrent = parrent;
        }

        public void DisplayCard()
        {
            Panel cardConteiner = new Panel() { Size = new Size(_parrent.Size.Width - 10, 100), BackColor = Color.Salmon, Parent = _parrent };

            Label firstName = new Label() { Text = "Имя - " + _student.FirstName, Parent = cardConteiner, AutoSize = true };
            firstName.Location = new Point(firstName.Location.X + 5, firstName.Location.Y + 5);

            Label lastName = new Label() { Text = "Фамилия - " + _student.LastName, Parent = cardConteiner, AutoSize = true };
            lastName.Location = new Point(lastName.Location.X + 5, lastName.Location.Y + 20);

            Label adress = new Label() { Text = "Адресс - " + _student.Adress, Parent = cardConteiner, AutoSize = true };
            adress.Location = new Point(adress.Location.X + 5, adress.Location.Y + 35);

            Label email = new Label() { Text = "Емайл - " + _student.Email, Parent = cardConteiner, AutoSize = true };
            email.Location = new Point(email.Location.X + 5, email.Location.Y + 50);

            Label phone = new Label() { Text = "Телефон - " + _student.Phone, Parent = cardConteiner, AutoSize = true };
            phone.Location = new Point(phone.Location.X + 5, phone.Location.Y + 65);

            Label id = new Label() { Text = "Идентификатор - " + _student.StudentId, Parent = cardConteiner, AutoSize = true };
            id.Location = new Point(id.Location.X + 5, id.Location.Y + 80);
        }
    }

    internal class TeacherCard
    {
        private Teacher _teacher;
        private FlowLayoutPanel _parrent;

        public TeacherCard(Teacher teacher, FlowLayoutPanel parrent)
        {
            _teacher = teacher;
            _parrent = parrent;
        }

        public void DisplayCard()
        {
            Panel cardConteiner = new Panel() { Size = new Size(_parrent.Size.Width - 10, 100), BackColor = Color.Salmon, Parent = _parrent };

            Label firstName = new Label() { Text = "Имя - " + _teacher.FirstName, Parent = cardConteiner, AutoSize = true };
            firstName.Location = new Point(firstName.Location.X + 5, firstName.Location.Y + 5);

            Label lastName = new Label() { Text = "Фамилия - " + _teacher.LastName, Parent = cardConteiner, AutoSize = true };
            lastName.Location = new Point(lastName.Location.X + 5, lastName.Location.Y + 20);

            Label adress = new Label() { Text = "Специализация - " + _teacher.Specialisation, Parent = cardConteiner, AutoSize = true };
            adress.Location = new Point(adress.Location.X + 5, adress.Location.Y + 35);

            Label email = new Label() { Text = "Емайл - " + _teacher.Email, Parent = cardConteiner, AutoSize = true };
            email.Location = new Point(email.Location.X + 5, email.Location.Y + 50);

            Label phone = new Label() { Text = "Телефон - " + _teacher.Phone, Parent = cardConteiner, AutoSize = true };
            phone.Location = new Point(phone.Location.X + 5, phone.Location.Y + 65);

            Label id = new Label() { Text = "Идентификатор - " + _teacher.TeacherId, Parent = cardConteiner, AutoSize = true };
            id.Location = new Point(id.Location.X + 5, id.Location.Y + 80);
        }
    }

    internal class CourseCard
    {
        private Course _course;
        private FlowLayoutPanel _parrent;

        public CourseCard(Course course, FlowLayoutPanel parrent)
        {
            _course = course;
            _parrent = parrent;
        }

        public void DisplayCard()
        {
            Panel cardConteiner = new Panel() { Size = new Size(_parrent.Size.Width - 10, 100), BackColor = Color.Salmon, Parent = _parrent };

            Label firstName = new Label() { Text = "Название - " + _course. CourseName, Parent = cardConteiner, AutoSize = true };
            firstName.Location = new Point(firstName.Location.X + 5, firstName.Location.Y + 5);

            Label lastName = new Label() { Text = "Описание - " + _course.Description, Parent = cardConteiner, AutoSize = true };
            lastName.Location = new Point(lastName.Location.X + 5, lastName.Location.Y + 20);

            Label adress = new Label() { Text = "Продолжительность - " + _course.Duration + " часов", Parent = cardConteiner, AutoSize = true };
            adress.Location = new Point(adress.Location.X + 5, adress.Location.Y + 35);

            Label email = new Label() { Text = "Цена - " + _course.Price + " рублей", Parent = cardConteiner, AutoSize = true };
            email.Location = new Point(email.Location.X + 5, email.Location.Y + 50);

            Label id = new Label() { Text = "Идентификатор - " + _course.CourseId, Parent = cardConteiner, AutoSize = true };
            id.Location = new Point(id.Location.X + 5, id.Location.Y + 65);
        }
    }

    internal class ClassCard
    {
        private Class _class;
        private FlowLayoutPanel _parrent;
        private EducationalCenterContext _context;

        public ClassCard(Class cls, FlowLayoutPanel parrent, EducationalCenterContext context)
        {
            _class = cls;
            _parrent = parrent;
            _context = context;
        }

        public void DisplayCard()
        {
            Panel cardConteiner = new Panel() { Size = new Size(_parrent.Size.Width - 10, 100), BackColor = Color.Salmon, Parent = _parrent };

            Label firstName = new Label() { Text = "Курс - " + _context.Courses.First(c => c.CourseId == _class.Course.CourseId).CourseName, Parent = cardConteiner, AutoSize = true };
            firstName.Location = new Point(firstName.Location.X + 5, firstName.Location.Y + 5);

            Label lastName = new Label() { Text = "Преподователь - " + _context.Teachers.First(c => c.TeacherId == _class.Teacher.TeacherId).FirstName, Parent = cardConteiner, AutoSize = true };
            lastName.Location = new Point(lastName.Location.X + 5, lastName.Location.Y + 20);

            Label startDate = new Label() { Text = "Дата начала - " + _class.StartDate, Parent = cardConteiner, AutoSize = true };
            startDate.Location = new Point(startDate.Location.X + 5, startDate.Location.Y + 35);

            Label endDate = new Label() { Text = "Дата конца - " + _class.EndDate, Parent = cardConteiner, AutoSize = true };
            endDate.Location = new Point(endDate.Location.X + 5, endDate.Location.Y + 50);

            Label duration = new Label() { Text = "Продолжительность - " + _class.Duration + " часов", Parent = cardConteiner, AutoSize = true };
            duration.Location = new Point(duration.Location.X + 5, duration.Location.Y + 65);

            Label id = new Label() { Text = "Идентификатор - " + _class.ClassId, Parent = cardConteiner, AutoSize = true };
            id.Location = new Point(id.Location.X + 5, id.Location.Y + 80);
        }
    }

    internal class RegistrationCard
    {
        private Registration _registration;
        private FlowLayoutPanel _parrent;
        private EducationalCenterContext _context;

        public RegistrationCard(Registration registration, FlowLayoutPanel parrent, EducationalCenterContext context)
        {
            _registration = registration;
            _parrent = parrent;
            _context = context;
        }

        public void DisplayCard()
        {
            Panel cardConteiner = new Panel() { Size = new Size(_parrent.Size.Width - 10, 100), BackColor = Color.Salmon, Parent = _parrent };

            Label clas = new Label() { Text = "Номер занятия - " + _context.Classes.First(c => c.ClassId == _registration.Class.ClassId).ClassId, Parent = cardConteiner, AutoSize = true };
            clas.Location = new Point(clas.Location.X + 5, clas.Location.Y + 5);

            Label student = new Label() { Text = "Студент - " + _context.Students.First(c => c.StudentId == _registration.Student.StudentId).FirstName, Parent = cardConteiner, AutoSize = true };
            student.Location = new Point(student.Location.X + 5, student.Location.Y + 20);

            Label registrationDate = new Label() { Text = "Дата регистрации - " + _registration.RegistrationDate, Parent = cardConteiner, AutoSize = true };
            registrationDate.Location = new Point(registrationDate.Location.X + 5, registrationDate.Location.Y + 35);

            Label mark = new Label() { Text = "Оценка - " + _registration.Mark, Parent = cardConteiner, AutoSize = true };
            mark.Location = new Point(mark.Location.X + 5, mark.Location.Y + 50);

            Label id = new Label() { Text = "Идентификатор - " + _registration.RegistrationId, Parent = cardConteiner, AutoSize = true };
            id.Location = new Point(id.Location.X + 5, id.Location.Y + 65);
        }
    }
}