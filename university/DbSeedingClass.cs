using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using university.Model;
using university.Services;

namespace university
{
    public static class DbSeedingClass
    {
        public static void SeedDataContext(this UniversityDbContext context)
        {
            //            //var bookTeachers = new List<BookTeacher>()
            //            //{
            //            //    new BookTeacher()
            //            //    {
            //            //        teachers = new Teachers()
            //            //        {
            //            //            FirstName = "Adel",
            //            //            LastName = "Mohamed",
            //            //            NameFamily = "Al-Asiri" ,
            //            //            DatePublished = new DateTime(2020,1,1),
            //            //            Specialties = new Specialties
            //            //            {
            //            //                section = new TheSections
            //            //               {
            //            //                 Name = "Computer"
            //            //               },

            //            //                Name = "Programming",
            //            //                DatePublished = new DateTime(2020,1,1),

            //            //                students = new List<Students>
            //            //                {
            //            //                  new Students { FirstName = "Khaled" , LastName = "Ali" , NameFamily = "Al-Shehri" , DatePublished = new DateTime(2020,1,1)},
            //            //                   new Students { FirstName = "Abdullah " , LastName = "Abdo " , NameFamily = "Al-Asiri" , DatePublished = new DateTime(2020,1,1)},
            //            //                    new Students { FirstName = "Ali" , LastName = "Abdullah" , NameFamily = "Al-Shehri" , DatePublished = new DateTime(2020,1,1)}
            //            //                } ,
            //            //            },
            //            //             departmentDirector = new DepartmentDirectors()
            //            //             {
            //            //                  FirstName = "Mansour",
            //            //                  LastName = "Mohammed",
            //            //                  NameFamily = "Al-Asiri",
            //            //                  DatePublished = new DateTime(2020,1,1),
            //            //             },
            //            //              teacherStudent = new List <TeacherStudents>()
            //            //              {
            //            //                  new TeacherStudents()
            //            //                  {
            //            //                     StudentId = 1
            //            //                  },
            //            //                  new TeacherStudents()
            //            //                  {
            //            //                     StudentId = 2
            //            //                  },
            //            //                  new TeacherStudents()
            //            //                  {
            //            //                     StudentId = 3
            //            //                  }
            //            //              }
            //            //        },
            //            //         books =  new Books()
            //            //         {
            //            //                     Name = "Programming 1",
            //            //                     Number = 123456 ,
            //            //                     DatePublished = new DateTime(2020,1,1),


            //            //         }
            //            //    }
            //            // };

            //            //context.BookTeachers.AddRange(bookTeachers);
            //            //context.SaveChanges();

            //            //    var specialties = new Specialties()
            //            //    {
            //            //        section = new TheSections()
            //            //        {
            //            //            Name = "Computer",
            //            //            DatePublished = new DateTime(2020, 1, 1)
            //            //        },

            //            //        Name = "Programming",
            //            //        DatePublished = new DateTime(2020, 1, 1),

            //            //        Supervisors = new List<Supervisor>()
            //            //            {
            //            //              new Supervisor()
            //            //              {
            //            //                   teacher = new Teachers()
            //            //                   {
            //            //                    FirstName = "Adel",
            //            //                    LastName = "Mohamed",
            //            //                    NameFamily = "Al-Asiri" ,
            //            //                    DatePublished = new DateTime(2020,1,1),
            //            //                    Specialties = new Specialties()
            //            //                    {
            //            //                      Id = 1
            //            //                     },
            //            //                    teacherStudent = new List<TeacherStudents>()
            //            //                    {
            //            //                        new TeacherStudents()
            //            //                        {
            //            //                           student = new Students()
            //            //                           {FirstName = "Yasser" , LastName = "Ali" , NameFamily = "Al-Ahmari" , DatePublished = new DateTime(2020,1,1), specialtie = new Specialties(){ Id = 1} },

            //            //                        } ,
            //            //                        new TeacherStudents()
            //            //                        {
            //            //                           student = new Students()
            //            //                           {FirstName = "Mohammed" , LastName = "Ali" , NameFamily = "Al-Shehri" , DatePublished = new DateTime(2020,1,1), specialtie = new Specialties(){ Id = 1} },

            //            //                        },

            //            //                        new TeacherStudents()
            //            //                        {
            //            //                           student = new Students()
            //            //                           {FirstName = "Abdo" , LastName = "Ali" , NameFamily = "Al-Shehri" , DatePublished = new DateTime(2020,1,1), specialtie = new Specialties(){ Id = 1} , },

            //            //                        }

            //            //                    },
            //            //                    BookTeachers = new List <BookTeacher>()
            //            //                    {
            //            //                       new BookTeacher()
            //            //                       {
            //            //                         books = new Books()
            //            //                         {
            //            //                             Name = "Programming Java" ,
            //            //                             Number = 123456,
            //            //                             DatePublished = new DateTime(2020,1,1),
            //            //                             BookStudents = new List<BookStudents>()
            //            //                             {
            //            //                                 new BookStudents()
            //            //                                 {
            //            //                                     students = new Students()
            //            //                                     {
            //            //                                           FirstName = "Khaled" , LastName = "Nasser" , NameFamily = "Al-Shehri" , DatePublished = new DateTime(2020,1,1),
            //            //                                 new BookStudents()
            //            //                                 {
            //            //                                     students = new Students()
            //            //                                     {
            //            //                                           FirstName = "Abdullah " , LastName = "Abdo " , NameFamily = "Al-Salami" , DatePublished = new DateTime(2020,1,1),
            //            //                                  new BookStudents()
            //            //                                 {
            //            //                                     students = new Students()
            //            //                                     {
            //            //                                           FirstName = "Ali" , LastName = "Abdullah" , NameFamily = "Al-Asiri" , DatePublished = new DateTime(2020,1,1),
            //            //                       } ,
            //            //                        new BookTeacher()
            //            //                       {
            //            //                         books = new Books()
            //            //                         {
            //            //                             Name = "Sql" ,
            //            //                             Number = 12458,
            //            //                             DatePublished = new DateTime(2020,1,1),
            //            //                          }
            //            //                        }
            //            //              } ,
            //            //                     departmentDirector = new DepartmentDirectors()
            //            //                     {
            //            //                          FirstName = "Mansour",
            //            //                          LastName = "Mohammed",
            //            //                          NameFamily = "Al-Asiri",
            //            //                          DatePublished = new DateTime(2020,1,1),
            //            //                     }
            //            //                   },
            //            //                   DatePublished = new DateTime(2020,1,1)
            //            //              }
            //            //            }
            //            //    };
            //            //    context.Specialties.Add(specialties);
            //            //    context.SaveChanges();
            //            //}

            //            //var bookTeachers = new List<BookTeacher>()
            //            //{
            //            //    new BookTeacher()
            //            //    {
            //            //        teachers = new Teachers()
            //            //        {
            //            //            FirstName = "Adel",
            //            //            LastName = "Mohamed",
            //            //            NameFamily = "Al-Asiri" ,
            //            //            DatePublished = new DateTime(2020,1,1),
            //            //            Specialties = new Specialties
            //            //            {
            //            //                section = new TheSections
            //            //               {
            //            //                 Name = "Computer"
            //            //               },

            //            //                Name = "Programming",
            //            //                DatePublished = new DateTime(2020,1,1),
            //            //            },
            //            //             departmentDirector = new DepartmentDirectors()
            //            //             {
            //            //                  FirstName = "Mansour",
            //            //                  LastName = "Mohammed",
            //            //                  NameFamily = "Al-Asiri",
            //            //                  DatePublished = new DateTime(2020,1,1),
            //            //             },
            //            //        },
            //            //         books =  new Books()
            //            //         {
            //            //                     Name = "Programming Java",
            //            //                     Number = 123456 ,
            //            //                     DatePublished = new DateTime(2020,1,1),
            //            //                     BookStudents = new List<BookStudents>
            //            //                     {
            //            //                         new BookStudents()
            //            //                         {
            //            //                           students = new Students()
            //            //                           {
            //            //                               FirstName = "Khaled" , LastName = "Ali" , NameFamily = "Al-Shehri" , DatePublished = new DateTime(2020,1,1),
            //            //                           }
            //            //                         },

            //            //                         new BookStudents()
            //            //                         {
            //            //                           students = new Students()
            //            //                           {
            //            //                              FirstName = "Abdullah " , LastName = "Abdo " , NameFamily = "Al-Asiri" , DatePublished = new DateTime(2020,1,1)
            //            //                           }
            //            //                         },

            //            //                         new BookStudents()
            //            //                         {
            //            //                           students = new Students()
            //            //                           {
            //            //                              FirstName = "Abdullah " , LastName = "Abdo " , NameFamily = "Al-Asiri" , DatePublished = new DateTime(2020,1,1)
            //            //                           }
            //            //                         }
            //            //                     }
            //            //         }
            //            //    }
            //            // };

            //            //context.BookTeachers.AddRange(bookTeachers);
            //            //context.SaveChanges();

            //            var bookteaher = new BookTeacher()
            //            {
            //                teachers = new Teachers()
            //                {
            //                    FirstName = "Adel",
            //                    LastName = "Mohamed",
            //                    NameFamily = "Al-Asiri",
            //                    DatePublished = new DateTime(2020, 1, 1),
            //                    Specialties = new Specialties()
            //                    {
            //                        Name = "Programming",
            //                        DatePublished = new DateTime(2020, 1, 1),
            //                        TheSections = new TheSections()
            //                        {
            //                            Name = "Computer",
            //                            DatePublished = new DateTime(2020, 1, 1),
            //                            DepartmentDirectors = new DepartmentDirectors()
            //                            {
            //                                FirstName = "Mansour",
            //                                LastName = "Mohammed",
            //                                NameFamily = "Al-Asiri",
            //                                DatePublished = new DateTime(2020, 1, 1),
            //                                Supervisor = new List<Supervisor>()
            //                                {
            //                                    new Supervisor()
            //                                    {
            //                                        FirstName = "Adel",
            //                                        LastName = "Mohamed",
            //                                        NameFamily = "Al-Asiri" ,
            //                                        DatePublished = new DateTime(2020,1,1),
            //                                        Students = new List<Students>()
            //                                        {
            //                                            new Students()
            //                                            {
            //                                                FirstName = "Abdullah",
            //                                                LastName = "Abdo",
            //                                                NameFamily = "Al-Asiri",
            //                                                DatePublished = new DateTime(2020, 1, 1),

            //                                            } ,
            //                                             new Students()
            //                                            {
            //                                                FirstName = "Yasser",
            //                                                LastName = "Ali",
            //                                                NameFamily = "Al-Asiri",
            //                                                DatePublished = new DateTime(2020, 1, 1),
            //                                                 BookStudents = new List<BookStudents>()
            //                                                {
            //                                                  new BookStudents()
            //                                                  {
            //                                                      books = new Books()
            //                                                      {
            //                                                          Name = "Programming Java",
            //                                                          Number = 123456 ,
            //                                                          DatePublished = new DateTime(2020,1,1),
            //                                                      }
            //                                                  }
            //                                                }

            //                                            } ,
            //                                              new Students()
            //                                            {
            //                                                FirstName = "Ali",
            //                                                LastName = "Nasser",
            //                                                NameFamily = "Asiri",
            //                                                DatePublished = new DateTime(2020, 1, 1),
            //                                                BookStudents = new List<BookStudents>()
            //                                                {
            //                                                  new BookStudents()
            //                                                  {
            //                                                      books = new Books()
            //                                                      {
            //                                                          Name = "Programming Java",
            //                                                          Number = 123456 ,
            //                                                          DatePublished = new DateTime(2020,1,1),
            //                                                      }
            //                                                  }
            //                                                }
            //                                            }
            //                                        }
            //                                    } 
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            };


            //            context.BookTeachers.Add(bookteaher);
            //            context.SaveChanges();

            var book = new Books
            {
                Name = "Programming Java",
                Number = 123456,
                DatePublished = new DateTime(2020, 1, 1),
                
                specialtie = new Specialties
                {
                    Name = "Programming",
                    DatePublished = new DateTime(2020, 1, 1),
                    TheSections = new TheSections
                    {
                        Name = "Computer",
                        DatePublished = new DateTime(2020, 1, 1),
                        
                        DepartmentDirectors = new DepartmentDirectors
                        {
                            FirstName = "Mansour",
                            LastName = "Mohammed",
                            NameFamily = "Al-Asiri",
                            DatePublished = new DateTime(2020, 1, 1),
                        }
                    }
                } ,
                 BookTeacherStudents = new List<BookTeacherStudent>()
                 {
                     new BookTeacherStudent
                     {
                         teachers = new Teachers
                         {
                             FirstName = "Adel",
                             LastName = "Mohamed",
                             NameFamily = "Al-Asiri",
                             DatePublished = new DateTime(2020, 1, 1),
                         } ,
                         student = new Students
                         {
                              FirstName = "Ali",
                              LastName = "Nasser",
                              NameFamily = "Asiri",
                              DatePublished = new DateTime(2020, 1, 1),
                              supervisor = new Supervisor
                              {
                                  FirstName = "Adel",
                                  LastName = "Mohamed",
                                  NameFamily = "Al-Asiri" ,
                                  DatePublished = new DateTime(2020,1,1),
                              }
                         } 
                     },
                     //new BookTeacherStudent
                     //{
                     //     student = new Students
                     //     {
                     //         FirstName = "Yasser",
                     //         LastName = "Ali",
                     //         NameFamily = "Al-Asiri",
                     //         DatePublished = new DateTime(2020, 1, 1),
                     //         supervisor = new Supervisor
                     //         {
                     //             FirstName = "Mohamed",
                     //             LastName = "Ali",
                     //             NameFamily = "Al-Shehri" ,
                     //             DatePublished = new DateTime(2020,1,1),
                     //         }
                     //     }
                     //},
                     //new BookTeacherStudent
                     //{
                     //     student = new Students
                     //     {
                     //         FirstName = "Abdullah",
                     //         LastName = "Abdo",
                     //         NameFamily = "Al-Asiri",
                     //         DatePublished = new DateTime(2020, 1, 1),
                     //         supervisor = new Supervisor
                     //         {
                     //             FirstName = "Abdurhman",
                     //             LastName = "Nasser",
                     //             NameFamily = "Al-Shehri" ,
                     //             DatePublished = new DateTime(2020,1,1),
                     //         }
                     //     }
                     //}
                 } 
            };

            context.Books.Add(book);
            context.SaveChanges();
        }
        }
}
