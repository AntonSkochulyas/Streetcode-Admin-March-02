using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Services.BlobStorageService;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
using Streetcode.DAL.Entities.Analytics;
using Streetcode.DAL.Entities.Dictionaries;
using Streetcode.DAL.Entities.Feedback;
using Streetcode.DAL.Entities.InfoBlocks;
using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Entities.Media;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Entities.Streetcode.TextContent;
using Streetcode.DAL.Entities.Streetcode.Types;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.DAL.Entities.Transactions;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.WebApi.Extensions
{
    public static class SeedingLocalExtension
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                //Directory.CreateDirectory(app.Configuration.GetValue<string>("Blob:BlobStorePath"));
                var dbContext = scope.ServiceProvider.GetRequiredService<StreetcodeDbContext>();
                var blobOptions = app.Services.GetRequiredService<IOptions<BlobEnvironmentVariables>>();

                //string blobPath = app.Configuration.GetValue<string>("Blob:BlobStorePath");
                var repo = app.Services.GetRequiredService<IRepositoryWrapper>();
                var logger = app.Services.GetRequiredService<ILoggerService>();

                //var blobService = app.Services.GetRequiredService<IBlobService>();
                //string initialDataImagePath = "../Streetcode.DAL/InitialData/images.json";
                //string initialDataAudioPath = "../Streetcode.DAL/InitialData/audios.json";

                //if (!dbContext.Images.Any())
                //{
                //    string imageJson = File.ReadAllText(initialDataImagePath, Encoding.UTF8);
                //    string audiosJson = File.ReadAllText(initialDataAudioPath, Encoding.UTF8);
                //    var imgfromJson = JsonConvert.DeserializeObject<List<Image>>(imageJson);
                //    var audiosfromJson = JsonConvert.DeserializeObject<List<Audio>>(audiosJson);

                //    foreach (var img in imgfromJson)
                //    {
                //        string filePath = Path.Combine(blobPath, img.BlobName);
                //        if (!File.Exists(filePath))
                //        {
                //            img.BlobName = blobService.SaveFileInStorage(img.Base64, img.BlobName.Split('.')[0], img.BlobName.Split('.')[1]);
                //        }
                //    }

                //    foreach (var audio in audiosfromJson)
                //    {
                //        string filePath = Path.Combine(blobPath, audio.BlobName);
                //        if (!File.Exists(filePath))
                //        {
                //            audio.BlobName = blobService.SaveFileInStorage(audio.Base64, audio.BlobName.Trim(), audio.BlobName.Split(".")[1]);
                //        }
                //    }

                //    dbContext.Images.AddRange(imgfromJson);

                //    await dbContext.SaveChangesAsync();

                //    if (!dbContext.Toponyms.Any())
                //    {
                //        dbContext.Toponyms.AddRange(
                //            new DAL.Entities.Toponyms.Toponym
                //            {
                //                Oblast = "Lvivska",
                //                AdminRegionOld = "Lvivska",
                //                AdminRegionNew = "Dniprovska",
                //                Gromada = "Dnipro",
                //                Community = "Sample Community 1",
                //                StreetName = "Sample Street 1",
                //                StreetType = "Avenue"
                //            },
                //            new DAL.Entities.Toponyms.Toponym
                //            {                     
                //                Oblast = "Kiev",
                //                AdminRegionOld = "Kiev",
                //                AdminRegionNew = "Kyiv",
                //                Gromada = "Kyiv",
                //                Community = "Sample Community 2",
                //                StreetName = "Sample Street 2",
                //                StreetType = "Road"
                //            },
                //            new DAL.Entities.Toponyms.Toponym
                //            {
                //                Oblast = "Odessa",
                //                AdminRegionOld = "Odessa",
                //                AdminRegionNew = "Odesa",
                //                Gromada = "Odesa",
                //                Community = "Sample Community 3",
                //                StreetName = "Sample Street 3",
                //                StreetType = "Boulevard"
                //            },
                //            new DAL.Entities.Toponyms.Toponym
                //            {
                //                Oblast = "Kharkiv",
                //                AdminRegionOld = "Kharkiv",
                //                AdminRegionNew = "Kharkiv",
                //                Gromada = "Kharkiv",
                //                Community = "Sample Community 4",
                //                StreetName = "Sample Street 4",
                //                StreetType = "Lane"
                //            },
                //            new DAL.Entities.Toponyms.Toponym
                //            {
                //                Oblast = "Dnipropetrovsk",
                //                AdminRegionOld = "Dnipropetrovsk",
                //                AdminRegionNew = "Dnipro",
                //                Gromada = "Dnipro",
                //                Community = "Sample Community 5",
                //                StreetName = "Sample Street 5",
                //                StreetType = "Street"
                //            },
                //            new DAL.Entities.Toponyms.Toponym
                //            {
                //                Oblast = "Zaporizhia",
                //                AdminRegionOld = "Zaporizhia",
                //                AdminRegionNew = "Zaporizhzhia",
                //                Gromada = "Zaporizhzhia",
                //                Community = "Sample Community 6",
                //                StreetName = "Sample Street 6",
                //                StreetType = "Avenue"
                //            },
                //            new DAL.Entities.Toponyms.Toponym
                //            {
                //                Oblast = "Vinnytsia",
                //                AdminRegionOld = "Vinnytsia",
                //                AdminRegionNew = "Vinnytsia",
                //                Gromada = "Vinnytsia",
                //                Community = "Sample Community 7",
                //                StreetName = "Sample Street 7",
                //                StreetType = "Boulevard"
                //            });

                //        await dbContext.SaveChangesAsync();
                //    }

                if (!dbContext.Responses.Any())
                {
                    dbContext.Responses.AddRange(
                        new Response
                        {
                            Name = "Alex",
                            Description = "Good Job",
                            Email = "dmytrobuchkovsky@gmail.com"
                        },
                        new Response
                        {
                            Name = "Danyil",
                            Description = "Nice project",
                            Email = "dt210204@gmail.com"
                        });

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.UsersAdditionalInfo.Any())
                {
                    var userAdditionalInfo = new UserAdditionalInfo()
                    {
                        Age = 18,
                        Email = "admin@gmail.com",
                        Phone = "+380630000200",
                        FirstName = "Admin first name",
                        SecondName = "Admin second name",
                        ThirdName = "Admin third name"
                    };

                    dbContext.Add(userAdditionalInfo);

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.AuthorShipHyperLinks.Any())
                {
                    dbContext.AuthorShipHyperLinks.AddRange(new AuthorShipHyperLink { Title = "Test 1", URL = "Test 1 authorship YAROSLAV BEST PYPSIK" },
                        new AuthorShipHyperLink { Title = "Test 2", URL = "Test 2 authorship NASTYA SMALL PYPSIK" });

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Articles.Any())
                {
                    dbContext.Articles.AddRange(new Article() { Text = "Article text 1 test", Title = "Articel test 1 text super womba bomba" }, new Article() { Text = "Article text 2 test", Title = "Articel test 2 text super womba bomba" });

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.AuthorShips.Any())
                {
                    dbContext.AuthorShips.AddRange(new AuthorShip() { Text = "BIbabumba authorship text 1", }, new AuthorShip() { Text = "BIbabumba authorship text 2", });

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.DictionaryItems.Any())
                {
                    dbContext.DictionaryItems.AddRange(new DictionaryItem() { Word = "NASTYA", Description = "PYPSIK" }, new DictionaryItem() { Word = "YAROSLAV", Description = "BEST PYPSIK" });

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.ApplicationUsers.Any())
                {
                    var userAdditionalInfo = await dbContext.UsersAdditionalInfo.FirstOrDefaultAsync();

                    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                    var adminUser = new ApplicationUser()
                    {
                        UserName = configuration["JWT:AdminLogin"],
                        UserAdditionalInfoId = userAdditionalInfo.Id,
                        UserAdditionalInfo = userAdditionalInfo
                    };

                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    var result = await userManager.CreateAsync(adminUser, configuration["JWT:AdminPassword"]);

                    if (result.Succeeded)
                    {
                        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                        {
                            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                        }

                        if (!await roleManager.RoleExistsAsync(UserRoles.User))
                        {
                            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                        }

                        await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);

                        await dbContext.SaveChangesAsync();
                    }
                }

                if (!dbContext.Terms.Any())
                {
                    dbContext.Terms.AddRange(
                        new Term
                        {
                            Title = "етнограф",
                            Description = "Етнографія — суспільствознавча наука, об'єктом дослідження якої є народи, їхня культура і побут, походження, розселення," +
                            " процеси культурно-побутових відносин на всіх етапах історії людства."
                        },
                        new Term
                        {
                            Title = "гравер",
                            Description = "Гра́фіка — вид образотворчого мистецтва, для якого характерна перевага ліній і штрихів, використання контрастів білого та" +
                                " чорного та менше, ніж у живописі, використання кольору. Твори можуть мати як монохромну, так і поліхромну гаму."
                        },
                        new Term
                        {
                            Title = "кріпак",
                            Description = "Кріпа́цтво, або кріпосне́ право, у вузькому сенсі — правова система, або система правових норм при феодалізмі, яка встановлювала" +
                                " залежність селянина від феодала й неповну власність феодала на селянина."
                        },
                        new Term
                        {
                            Title = "мачуха",
                            Description = "Ма́чуха — нерідна матір для дітей чоловіка від його попереднього шлюбу.",
                        });

                    await dbContext.SaveChangesAsync();

                    if (!dbContext.RelatedTerms.Any())
                    {
                        dbContext.RelatedTerms.AddRange(
                            new RelatedTerm
                            {
                                Word = "кріпаків",
                                TermId = 3,
                            });

                        await dbContext.SaveChangesAsync();
                    }
                }

                if (!dbContext.TeamMembers.Any())
                {
                    dbContext.AddRange(
                        new TeamMember
                        {
                            FirstName = "Inna",
                            LastName = "Krupnyk",
                            ImageId = null,
                            Description = "У 1894 році Грушевський за рекомендацією Володимира Антоновича призначений\r\nна посаду ординарного професора",
                            IsMain = true
                        },
                        new TeamMember
                        {
                            FirstName = "Danyil",
                            LastName = "Terentiev",
                            ImageId = null,
                            Description = "У 1894 році Грушевський за рекомендацією Володимира Антоновича призначений\r\nна посаду ординарного професора",
                            IsMain = true
                        },
                        new TeamMember
                        {
                            FirstName = "Nadia",
                            ImageId = null,
                            LastName = "Kischchuk",
                            Description = "У 1894 році Грушевський за рекомендацією Володимира Антоновича призначений\r\nна посаду ординарного професора",
                            IsMain = true

                        });

                    await dbContext.SaveChangesAsync();

                    if (!dbContext.Positions.Any())
                    {
                        dbContext.Positions.AddRange(
                            new Positions
                            {
                                Position = "Голова і засновниця ГО"
                            });

                        await dbContext.SaveChangesAsync();

                        if (!dbContext.TeamMemberPosition.Any())
                        {
                            dbContext.TeamMemberPosition.AddRange(
                                new TeamMemberPositions
                                {
                                    PositionsId = 1,
                                    TeamMemberId = 1
                                },
                                new TeamMemberPositions
                                {
                                    PositionsId = 1,
                                    TeamMemberId = 2
                                },
                                new TeamMemberPositions
                                {
                                    PositionsId = 1,
                                    TeamMemberId = 3
                                });

                            await dbContext.SaveChangesAsync();
                        }

                        if (!dbContext.TeamMemberLinks.Any())
                        {
                            dbContext.AddRange(
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.YouTube,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 1,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.Facebook,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 1,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.Instagram,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 1,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.Twitter,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 1,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.YouTube,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 2,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.Facebook,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 2,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.Instagram,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 2,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.Twitter,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 2,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.YouTube,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 3,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.Facebook,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 3,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.Instagram,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 3,
                                },
                                new TeamMemberLink
                                {
                                    LogoType = LogoType.Twitter,
                                    TargetUrl = "https://www.youtube.com/watch?v=8kCnOqvmEp0&ab_channel=JL%7C%D0%AE%D0%9B%D0%86%D0%AF%D0%9B%D0%A3%D0%A9%D0%98%D0%9D%D0%A1%D0%AC%D0%9A%D0%90",
                                    TeamMemberId = 3,
                                });
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }

                // Change User to UserAdditionalInfo after JWT Authorization will be done
                //if (!dbContext.Users.Any())
                //{
                //    dbContext.Users.AddRange(
                //        new DAL.Entities.Users.User
                //        {
                //            Email = "admin",
                //            Role = UserRole.MainAdministrator,
                //            Login = "admin",
                //            Name = "admin",
                //            Password = "admin",
                //            Surname = "admin",
                //        });

                //    await dbContext.SaveChangesAsync();
                //}

                if (!dbContext.News.Any())
                {
                    dbContext.News.AddRange(
                        new DAL.Entities.News.News
                        {
                            Title = "27 квітня встановлюємо перший стріткод!",
                            Text = "<p>Встановлення таблички про Михайла Грушевського в м. Київ стало важливою подією для киян та гостей столиці. Вона не лише прикрашає вулицю міста, а й нагадує про значний внесок цієї визначної особистості в історію України. Це також сприяє розповсюдженню знань про Михайла Грушевського серед широкого загалу, виховує національну свідомість та гордість за власну культуру.\r\n\r\nВстановлення таблички про Михайла Грушевського в Києві є важливим кроком на шляху вшанування відомих особистостей, які внесли вагомий внесок у розвиток України. Це також показує, що в Україні дбають про збереження національної спадщини та визнання внеску видатних історичних постатей в формування національної ідентичності.\r\n\r\nУрочисте встановлення таблички про Михайла Грушевського відбулося за участі високопосадовців міста, представників наукової спільноти та громадськості. Під час церемонії відбулися промови, в яких відзначили важливість дослідницької та літературної діяльності М. Грушевського, його внесок у вивчення історії України та роль у національному відродженні.\r\n\r\nМихайло Грушевський жив і працював в Києві на початку ХХ століття. Він був визнаний одним з провідних істориків свого часу, який досліджував історію України з наукової та національної позицій. Його праці були визнані авторитетними не лише в Україні, але й у світі, і мають велике значення для розуміння минулого та формування майбутнього українського народу.\r\n\r\nТабличка з відтвореним зображенням Михайла Грушевського стала вагомим символом вшанування цієї видатної постаті. Вона стала візитівкою Києва та пам'яткою культурної спадщини України, яка привертає увагу мешканців та гостей міста. Це важливий крок на шляху до збереження національної історії, культури та національної свідомості в Україні.\r\n\r\nВстановлення таблички про Михайла Грушевського в Києві свідчить про важливість визнання історичної спадщини та внеску видатних постатей в національну свідомість. Це також є визнанням ролі М. Грушевського у формуванні української національної ідентичності та його внеску в розвиток наукової та культурної спадщини України.\r\n\r\nТабличка була встановлена на видному місці в центрі Києва, недалеко від місця, де розташовується будинок, в якому колись проживав Михайло Грушевський. Зображення на табличці передає фотографію видатного історика, а також містить кратку інформацію про його життя та діяльність.\r\n\r\nМешканці та гості Києва високо оцінюють встановлення таблички про Михайла Грушевського, яке стало ще одним кроком на шляху до вшанування історичної спадщини України. Це також важливий крок у визнанні ролі українських науковців та культурних діячів у світовому контексті.\r\n\r\nВстановлення таблички про Михайла Грушевського в Києві є однією з ініціатив, спрямованих на підтримку і розширення національної пам'яті та відтворення історичної правди. Це важливий крок на шляху до відродження національної свідомості та підкреслення значення української культурної спадщини в світовому контексті.</p>",
                            URL = "first-streetcode",

                            CreationDate = DateTime.Now,
                        },
                        new DAL.Entities.News.News
                        {
                            Title = "Новий учасник команди!",
                            Text = "<p>Привітаймо нового учасника команди - Терентьєва Даниїла!. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque arcu orci, dictum at posuere a, tincidunt sit amet nibh. Donec pellentesque ac mauris tristique egestas. Vestibulum hendrerit eget nisi non viverra. Nullam ultricies sapien ac ipsum ullamcorper tristique. Mauris auctor, sapien vitae molestie ornare, libero orci fringilla velit, sed pharetra nibh augue id tellus. Mauris pulvinar vel felis convallis molestie. Integer mauris felis, ultrices nec vestibulum at, ullamcorper eu massa. Proin posuere consectetur facilisis. Nunc volutpat dictum massa, ac volutpat nisl malesuada nec.\r\n\r\nNulla nec felis quis metus efficitur efficitur ac nec est. Nulla eros quam, tincidunt at elit nec, iaculis eleifend sem. Pellentesque id sem id erat mollis fermentum non at ipsum. Donec justo ante, commodo a pharetra a, consectetur at urna. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Praesent porta, odio sed venenatis posuere, felis nibh finibus dui, placerat molestie dui libero at nisi. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Duis quis nisi in nisi pulvinar ultrices.\r\n\r\nPellentesque ante nunc, mattis vitae iaculis id, sollicitudin nec tortor. Pellentesque eu lectus suscipit, sodales nunc eu, lobortis enim. Praesent tempus dolor et felis vulputate hendrerit. Nunc ut lacus.</p>",
                            URL = "danya",

                            CreationDate = DateTime.Now,
                        },
                        new DAL.Entities.News.News
                        {
                            Title = "Новий учасник команди!",
                            Text = "<p>Привітаймо нового учасника команди - Скам Мастера!. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque arcu orci, dictum at posuere a, tincidunt sit amet nibh. Donec pellentesque ac mauris tristique egestas. Vestibulum hendrerit eget nisi non viverra. Nullam ultricies sapien ac ipsum ullamcorper tristique. Mauris auctor, sapien vitae molestie ornare, libero orci fringilla velit, sed pharetra nibh augue id tellus. Mauris pulvinar vel felis convallis molestie. Integer mauris felis, ultrices nec vestibulum at, ullamcorper eu massa. Proin posuere consectetur facilisis. Nunc volutpat dictum massa, ac volutpat nisl malesuada nec.\r\n\r\nNulla nec felis quis metus efficitur efficitur ac nec est. Nulla eros quam, tincidunt at elit nec, iaculis eleifend sem. Pellentesque id sem id erat mollis fermentum non at ipsum. Donec justo ante, commodo a pharetra a, consectetur at urna. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Praesent porta, odio sed venenatis posuere, felis nibh finibus dui, placerat molestie dui libero at nisi. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Duis quis nisi in nisi pulvinar ultrices.\r\n\r\nPellentesque ante nunc, mattis vitae iaculis id, sollicitudin nec tortor. Pellentesque eu lectus suscipit, sodales nunc eu, lobortis enim. Praesent tempus dolor et felis vulputate hendrerit. Nunc ut lacus.</p>",
                            URL = "scum",

                            CreationDate = DateTime.Now,
                        });

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Audios.Any())
                {
                    //dbContext.Audios.AddRange(audiosfromJson);

                    //await dbContext.SaveChangesAsync();

                    if (!dbContext.Streetcodes.Any())
                    {
                        dbContext.Streetcodes.AddRange(
                            new PersonStreetcode
                            {
                                Index = 1,
                                TransliterationUrl = "taras-shevchenko",
                                Teaser = "Тара́с Григо́рович Шевче́нко (25 лютого (9 березня) 1814, с. Моринці, Київська губернія," +
                         " Російська імперія (нині Звенигородський район, Черкаська область, Україна) — 26 лютого (10 березня) 1861, " +
                         "Санкт-Петербург, Російська імперія) — український поет, прозаїк, мислитель, живописець, гравер, етнограф, громадський діяч. " +
                         "Національний герой і символ України. Діяч українського національного руху, член Кирило-Мефодіївського братства. " +
                         "Академік Імператорської академії мистецтв",
                                ViewCount = 0,
                                CreatedAt = DateTime.Now,
                                DateString = "9 березня 1814 — 10 березня 1861",
                                EventStartOrPersonBirthDate = new DateTime(1814, 3, 9),
                                EventEndOrPersonDeathDate = new DateTime(1861, 3, 10),
                                FirstName = "Тарас",
                                Rank = "Григорович",
                                LastName = "Шевченко",
                                Title = "Тарас Шевченко",
                                Alias = "Кобзар",

                                // AudioId = 1,
                                Status = StreetcodeStatus.Published
                            },
                            new PersonStreetcode
                            {
                                Index = 2,
                                TransliterationUrl = "roman-ratushnyi",
                                Teaser = "Роман був з тих, кому не байдуже. Небайдуже до свого Протасового Яру та своєї України. Талановитий, щедрий, запальний. З нового покоління українців, народжених за незалежності, мета яких — краща Україна. Інтелектуал, активіст, громадський діяч. Бунтар проти несправедливості: корупції, свавілля. Невтомний як у боротьбі з незаконною забудовою, так і в захисті рідної країни від ворога. Учасник Помаранчевої революції 2004 року та Революції гідності 2013–2014-го. Воїн, який заради України пожертвував власним життям.",
                                ViewCount = 1,
                                CreatedAt = DateTime.Now,
                                DateString = "5 липня 1997 – 9 червня 2022",
                                EventStartOrPersonBirthDate = new DateTime(1997, 7, 5),
                                EventEndOrPersonDeathDate = new DateTime(2022, 6, 9),
                                FirstName = "Роман",
                                LastName = "Ратушний",
                                Title = "Роман Ратушний (Сенека)",
                                Alias = "Сенека",

                                //AudioId = 2,
                                Status = StreetcodeStatus.Published
                            });

                        await dbContext.SaveChangesAsync();

                        if (!dbContext.Subtitles.Any())
                        {
                            dbContext.Subtitles.AddRange(
                                new Subtitle
                                {
                                    SubtitleText = "Developers: StreedCodeTeam, made with love and passion, some more text, and more text. There was Danya",
                                    StreetcodeId = 1
                                },
                                new Subtitle
                                {
                                    SubtitleText = "Developers: StreedCodeTeam, made with love and passion, some more text, and more text. There was Danya",
                                    StreetcodeId = 2
                                });

                            await dbContext.SaveChangesAsync();
                        }

                        if (!dbContext.StreetcodeCoordinates.Any())
                        {
                            dbContext.StreetcodeCoordinates.AddRange(
                                new StreetcodeCoordinate
                                {
                                    Latitude = 49.8429M,
                                    Longtitude = 24.0311M,
                                    StreetcodeId = 1
                                },
                                new StreetcodeCoordinate
                                {
                                    Latitude = 50.4550M,
                                    Longtitude = 30.5238M,
                                    StreetcodeId = 2
                                });

                            await dbContext.SaveChangesAsync();
                        }

                        if (!dbContext.Videos.Any())
                        {
                            dbContext.Videos.AddRange(
                                new Video
                                {
                                    Title = "audio1",
                                    Description = "for streetcode1",
                                    Url = "https://www.youtube.com/watch?v=VVFEi6lTpZk&ab_channel=%D0%9E%D1%81%D1%82%D0%B0%D0%BD%D0%BD%D1%96%D0%B9%D0%93%D0%B5%D1%82%D1%8C%D0%BC%D0%B0%D0%BD",
                                    StreetcodeId = 1
                                },
                                new Video
                                {
                                    Title = "Біографія Т.Г.Шевченка",
                                    Url = "https://www.youtube.com/watch?v=YuoaECXH2Bc&ab_channel=%D0%A2%D0%B2%D0%BE%D1%8F%D0%9F%D1%96%D0%B4%D0%BF%D1%96%D0%BB%D1%8C%D0%BD%D0%B0%D0%93%D1%83%D0%BC%D0%B0%D0%BD%D1%96%D1%82%D0%B0%D1%80%D0%BA%D0%B0",
                                    StreetcodeId = 2
                                });

                            await dbContext.SaveChangesAsync();
                        }

                        if (!dbContext.Images.Any())
                        {
                            dbContext.Images.AddRange(new Image() { MimeType = "First", BlobName = "First" }, new Image() { MimeType = "Second", BlobName = "Second" }, new Image() { MimeType = "Third", BlobName = "Third" });
                            await dbContext.SaveChangesAsync();
                        }

                        if (!dbContext.Partners.Any())
                        {
                            dbContext.Partners.AddRange(
                                new Partner
                                {
                                    IsKeyPartner = true,
                                    Title = "SoftServe",
                                    Description = "Український культурний фонд є флагманською українською інституцією культури, яка у своїй діяльності інтегрує" +
                                        " різні види мистецтва – від сучасного мистецтва, нової музики й театру до літератури та музейної справи." +
                                        " Мистецький арсенал є флагманською українською інституцією культури, яка у своїй діяльності інтегрує різні" +
                                        " види мистецтва – від сучасного мистецтва, нової музики й театру до літератури та музейної справи.",

                                    LogoId = 1,
                                    TargetUrl = "https://www.softserveinc.com/en-us",
                                    UrlTitle = "go to SoftServe page"
                                },
                                new Partner
                                {
                                    Title = "Parimatch",
                                    Description = "Конторка для лошків з казіничами та лохотроном, аби стягнути побільше бабок з довірливих дурбобиків",

                                    LogoId = 2,
                                    TargetUrl = "https://parimatch.com/"
                                },
                                new Partner
                                {
                                    Title = "comunity partner",
                                    Description = "Класна платформа, я зацінив, а ти?",

                                    LogoId = 3,
                                    TargetUrl = "https://partners.salesforce.com/pdx/s/?language=en_US&redirected=RGSUDODQUL"
                                });

                            await dbContext.SaveChangesAsync();

                            if (!dbContext.PartnerSourceLinks.Any())
                            {
                                dbContext.PartnerSourceLinks.AddRange(
                                    new PartnerSourceLink
                                    {
                                        LogoType = LogoType.Twitter,
                                        TargetUrl = "https://twitter.com/SoftServeInc",
                                        PartnerId = 1
                                    },
                                    new PartnerSourceLink
                                    {
                                        LogoType = LogoType.Instagram,
                                        TargetUrl = "https://www.instagram.com/softserve_people/",
                                        PartnerId = 1
                                    },
                                    new PartnerSourceLink
                                    {
                                        LogoType = LogoType.Facebook,
                                        TargetUrl = "https://www.facebook.com/SoftServeCompany",
                                        PartnerId = 1
                                    });

                                await dbContext.SaveChangesAsync();
                            }

                            if (!dbContext.StreetcodePartners.Any())
                            {
                                dbContext.StreetcodePartners.AddRange(
                                    new StreetcodePartner
                                    {
                                        StreetcodeId = 2,
                                        PartnerId = 1
                                    },
                                    new StreetcodePartner
                                    {
                                        StreetcodeId = 2,
                                        PartnerId = 2
                                    },
                                    new StreetcodePartner
                                    {
                                        StreetcodeId = 2,
                                        PartnerId = 3
                                    },
                                    new StreetcodePartner
                                    {
                                        StreetcodeId = 1,
                                        PartnerId = 1
                                    },
                                    new StreetcodePartner
                                    {
                                        StreetcodeId = 1,
                                        PartnerId = 2
                                    },
                                    new StreetcodePartner
                                    {
                                        StreetcodeId = 1,
                                        PartnerId = 3
                                    });

                                await dbContext.SaveChangesAsync();
                            }
                        }

                        if (!dbContext.Arts.Any())
                        {
                            dbContext.Arts.AddRange(
                                new Art
                                {
                                    Title = "Анатолій Федірко",
                                    Description = "Анатолій Федірко, «Український супрематичний політичний діяч Михайло Грушевський», 2019-2020 роки."
                                },
                                new Art
                                {
                                    Title = "Анатолій Федірко",
                                    Description = "Анатолій Федірко, «Український супрематичний політичний діяч Михайло Грушевський», 2019-2020 роки."
                                },
                                new Art
                                {
                                    Title = "Назар Дубів",
                                    Description = "Назар Дубів опублікував серію малюнків, у яких перетворив класиків української літератури та політичних діячів на сучасних модників"
                                },
                                new Art
                                {
                                    Title = "Козаки на орбіті",
                                    Description = "«Козаки на орбіті» поєднує не тільки тему козаків, а й апелює до космічної тематики."
                                },
                                new Art
                                {
                                    Title = "Січових стрільців",
                                    Description = "На вулиці Січових стрільців, 75 закінчили малювати мурал Михайла Грушевського на місці малюнка будинку з лелекою."
                                },
                                new Art
                                {
                                    Title = "Січових стрільців",
                                    Description = "Some Description"
                                },
                                new Art
                                {
                                    Title = "Січових стрільців",
                                    Description = "Some Description"
                                },
                                new Art
                                {
                                    Title = "Січових стрільців",
                                    Description = "Some Description"
                                },
                                new Art
                                {
                                    Title = "Січових стрільців",
                                    Description = "Some Description"
                                });

                            await dbContext.SaveChangesAsync();

                            if (!dbContext.StreetcodeArts.Any())
                            {
                                dbContext.StreetcodeArts.AddRange(
                                    new StreetcodeArt
                                    {
                                        ArtId = 1,
                                        StreetcodeId = 1,
                                        Index = 1,
                                    },
                                    new StreetcodeArt
                                    {
                                        ArtId = 2,
                                        StreetcodeId = 1,
                                        Index = 2,
                                    },
                                    new StreetcodeArt
                                    {
                                        ArtId = 3,
                                        StreetcodeId = 1,
                                        Index = 3,
                                    },
                                    new StreetcodeArt
                                    {
                                        ArtId = 4,
                                        StreetcodeId = 1,
                                        Index = 4,
                                    },
                                    new StreetcodeArt
                                    {
                                        ArtId = 5,
                                        StreetcodeId = 1,
                                        Index = 5,
                                    },
                                    new StreetcodeArt
                                    {
                                        ArtId = 6,
                                        StreetcodeId = 1,
                                        Index = 6,
                                    },
                                    new StreetcodeArt
                                    {
                                        ArtId = 7,
                                        StreetcodeId = 2,
                                        Index = 1,
                                    },
                                    new StreetcodeArt
                                    {
                                        ArtId = 4,
                                        StreetcodeId = 2,
                                        Index = 2,
                                    },
                                    new StreetcodeArt
                                    {
                                        ArtId = 5,
                                        StreetcodeId = 2,
                                        Index = 3,
                                    },
                                    new StreetcodeArt
                                    {
                                        ArtId = 6,
                                        StreetcodeId = 2,
                                        Index = 4,
                                    });

                                await dbContext.SaveChangesAsync();
                            }
                        }

                        if (!dbContext.Texts.Any())
                        {
                            dbContext.Texts.AddRange(
                                new Text
                                {
                                    Title = "Дитинство та юність",
                                    TextContent = @"Тарас Шевченко народився 9 березня 1814 року в селі Моринці Пединівської волості Звенигородського повіту Київської губернії. Був третьою дитиною селян-кріпаків Григорія Івановича Шевченка та Катерини Якимівни після сестри Катерини (1804 — близько 1848) та брата Микити (1811 — близько 1870).

                    За родинними переказами, Тарасові діди й прадіди з батьківського боку походили від козака Андрія, який на початку XVIII століття прийшов із Запорізької Січі. Батьки його матері, Катерини Якимівни Бойко, були переселенцями з Прикарпаття.

                    1816 року сім'я Шевченків переїхала до села Кирилівка Звенигородського повіту, звідки походив Григорій Іванович. Дитячі роки Тараса пройшли в цьому селі. 1816 року народилася Тарасова сестра Ярина, 1819 року — сестра Марія, а 1821 року народився Тарасів брат Йосип.

                    Восени 1822 року Тарас Шевченко почав учитися грамоти у дяка Совгиря. Тоді ж ознайомився з творами Григорія Сковороди.

                    10 лютого 1823 року його старша сестра Катерина вийшла заміж за Антона Красицького — селянина із Зеленої Діброви, а 1 вересня 1823 року від тяжкої праці й злиднів померла мати Катерина. 

                    19 жовтня 1823 року батько одружився вдруге з удовою Оксаною Терещенко, в якої вже було троє дітей. Вона жорстоко поводилася з нерідними дітьми, зокрема з малим Тарасом. 1824 року народилася Тарасова сестра Марія — від другого батькового шлюбу.

                    Хлопець чумакував із батьком. Бував у Звенигородці, Умані, Єлисаветграді (тепер Кропивницький). 21 березня (2 квітня) 1825 року батько помер, і невдовзі мачуха повернулася зі своїми трьома дітьми до Моринців. Зрештою Тарас змушений був залишити домівку. Деякий час Тарас жив у свого дядька Павла, який після смерті його батька став опікуном сиріт. Дядько Павло був «великий катюга»; Тарас працював у нього, разом із наймитом у господарстві, але у підсумку не витримав тяжких умов життя й пішов у найми до нового кирилівського дяка Петра Богорського.

                    Як попихач носив воду, опалював школу, обслуговував дяка, читав псалтир над померлими і вчився. Не стерпівши знущань Богорського й відчуваючи великий потяг до живопису, Тарас утік від дяка й почав шукати в навколишніх селах учителя-маляра. Кілька днів наймитував і «вчився» малярства в диякона Єфрема. Також мав учителів-малярів із села Стеблева, Канівського повіту та із села Тарасівки Звенигородського повіту. 1827 року він пас громадську отару в Кирилівці й там зустрічався з Оксаною Коваленко. Згодом подругу свого дитинства поет не раз згадає у своїх творах і присвятить їй поему «Мар'яна-черниця».",

                                    StreetcodeId = 1
                                },
                                new Text
                                {
                                    Title = "Бунтар чи громадянин доброї волі?",
                                    TextContent = "Юний, харизматичний. Громадський активіст родом з Києва, а духом точно з якоїсь козацької колиски на кшталт Холодного Яру. З його доброї волі йому вдавалося все чи майже все, за що він брався за свої неповні 25. Для багатьох Роман Ратушний втілював надію на краще майбутнє та результативне лідерство. Надію на подолання корупції, лідерство в протистоянні незаконній забудові законними методами. \r\n \r\nВін був бунтарем. Але йшлося не про юнацький максималізм чи протест заради протесту. Роман бунтував проти несправедливості. Мафіозна дійсність, корупція та свавілля влади, окупант на твоїй землі. Захистити історичну спадщину чи згуртувати потужну громаду — таким було громадянське лицарство Ратушного.\r\n\r\nРідний Київ хлопець обожнював. Україну щиро любив. І з візій кращого майбутнього постійно народжувалися різні проєкти та ініціативи. Зокрема, Роман активно виступав за дерусифікацію: «Випалюйте в собі всю російську субкультуру. Інакше це все випалить вас».\r\n\r\nСаме в любові до Києва Роман ініціював та створив ГО «Захистимо Протасів Яр». Усе починалося як протест місцевої громади проти побудови на історичних схилах 40-поверхівок. А переросло в об’єднання, яке захищає права киян. І виграє суди у великих заангажованих бізнес-структур. Так сталося в 2021 році, коли Господарський суд визнав недійсним договір суборенди земельної ділянки. А мер Києва пізніше підтвердив наміри створити тут парк. Парк, де на честь Роми пообіцяли висадити дуби його колеги та друзі. \r\n\r\nПомаранчева революція, Революція гідності, боротьба із незаконною забудовою у Протасовому Яру. Юнак брав участь практично в усіх великих заходах, мітингах, акціях, де йшлося про боротьбу за справедливість. За словами батьків Романа, вони не виховували активного громадянина навмисне, він таким народився. «Думаю, такі люди з’являються на світ уже абсолютно досконалими. Про Рому я це відчувала і знала одразу. Це не є результат якогось виховання. Це абсолютно сформована особистість надзвичайно високого рівня. В усіх сенсах», — так сказала про сина письменниця Світлана Поваляєва.\r\n\r\nРоман змінив життя багатьох людей. Заради справедливості був готовий іти до кінця. Так, тато Тарас Ратушний про свого Романа каже: «Різниця між нами, моїм поколінням і нашими дітьми, в тому, що вони не зупиняються. Я не впевнений, усвідомлюють вони це чи ні, але якщо зважити ризики, якщо подумати, що станеться, можна програти. Тому треба діяти тут і зараз, до кінця. Ось де різниця. Ось про що Роман». Як і Роман, його батько приєднався до лав ЗСУ.\r\n\r\nПерші великі гроші юнак заробив завдяки тому, що обробляв мемуари відомої єврейської діячки. А до ГО «Захистимо Протасів Яр» якийсь час працював у комітеті Верховної Ради з питань житлово-комунальних послуг. І це йому, молодому студенту-правнику, було цікаво. Історія та право взагалі були на першому місці серед інтересів хлопця.\r\n\r\nВосени 2020 року Роман висунув свою кандидатуру на депутатство в Київраді, і хоч не пройшов тоді, але не засмутився. Це був його політичний досвід: зустрічався з виборцями, радив об’єднуватися в громади, закликав домовлятися одне з одним, щоб робити добрі справи. Роману вдавалося переконувати людей щирим словом.\r\n\r\nВін був різноплановою особистістю, швидко все опановував. За словами мами, рано почав ходити, швидко навчився говорити. А ще в його житті було багато музики, від фольклору до рок-н-ролу. Навчався в Джазовій академії Басюків на Оболоні разом зі старшим братом Василем, який у 2014 році, із початком російсько-української війни, поповнив лави ЗСУ. \r\n\r\n«Я знала Рому по всіх, напевно, найгучніших, найважливіших і найрезонансніших ініціативах, які сталися за останні кілька років. Він був прикладом і натхненням для чималого покоління, особливо — для молодих українців та українок», — каже активістка Марина Хромих, підкреслюючи масштабність Романа як людини. \r\n\r\nНа думку журналіста Дениса Казанського, Роман Ратушний був одним із найефективніших представників громадського сектору Києва. «Коли ми познайомилися, йому було 21–22 роки. Він надихав. Я вірив, що в нього велике політичне майбутнє. Радів, що в нас є такі люди».\r\n\r\nУ березні 2021 року Роман Ратушний разом з багатьма небайдужими мітингував проти незаконного увʼязнення Сергія Стерненка, одеського активіста. Побиті вікна Офісу Президента, розмальований фасад. Було сфабриковано відео, де це робить начебто Роман. Після публікації відео на сайті МВС Ратушного затримали та інкримінували групове хуліганство. В результаті — домашній арешт з електронним браслетом. Ратушний пов’язував таке рішення із «політичною» неприязню до нього з боку апарату Офісу Президента. За місяць завдяки зусиллям адвокатів Романа апеляційний суд зняв з нього всі обвинувачення.\r\n\r\nВипадків погроз про фізичну розправу над активістом Ратушним було безліч. Сам Роман пов’язував їх зі своєю діяльністю щодо захисту Протасового Яру та, зокрема, з компанією-забудовницею та особами-бенефіціарами. Так, хлопця намагалися страхати навіть відправкою на фронт за активну громадянську позицію. А він завжди відповідав у своїх численних інтерв’ю ЗМІ, що для нього захист Батьківщини не є покаранням.\r\n\r\nІ він пішов її захищати. З перших днів повномасштабного вторгнення. Думав про це й раніше, бо мав приклад брата. Спочатку служив у підрозділі «Протасового Яру» в обороні Києва. Згодом приєднався разом з кількома бойовими побратимами до 93-ї бригади на півночі Сумської області. Брав участь у деокупації Тростянця. Назва бригади — «Холодний Яр» — була натхненням для Романа, як історична пам’ять про опір українців загарбникам.\r\n\r\nПопри невеликий бойовий досвід, Ратушний став розвідником. Це одна з найнебезпечніших спеціалізацій через наближення впритул до ворога. 7 квітня 2022 року Роман опублікував на своїй фейсбук-сторінці військовий квиток як «план до кінця війни». Потім був Ізюмський напрямок. Через зв’язки в Києві постійно підбирав машини та обладнання для батальйону.\r\n\r\nУ своє останнє бойове завдання Роман підповз до позицій росіян і визначив розташування їхніх танків. Зміг розмінувати дорогу, але ворог його помітив. 9 червня 2022 року Роман Ратушний з позивним Сенека загинув у складі бойової групи. До останнього подиху Роман був «на самому вістрі. І навіть ще трішки попереду».\r\n\r\n«Рома не хотів би, щоб ми плакали. Він хотів би, щоб ми перемогли», — сказала мама Романа. А ще її син хотів, щоб на його могилі поставили козацький хрест, а на ньому як епітафію вибили вірш Михайля Семенка «Патагонія»:\r\n\r\nЯ не умру від смерти — \r\nЯ умру від життя. \r\nУмиратиму — життя буде мерти, \r\nНе маятиме стяг.\r\n\r\nЯ молодим, молодим умру —\r\nБо чи стану коли старим? \r\nЗалиш, залиш траурну гру. \r\nРозсип похоронні рими.\r\n\r\nЯ умру, умру в Патагонії дикій, \r\nБо належу огню й землі. \r\nРідні мої, я не чутиму ваших криків, \r\nЯ — нічий, поет світових слів.\r\n\r\nЯ умру в хвилю, коли природа стихне, \r\nЧекаючи на останню горобину ніч. \r\nЯ умру в павзу, коли серце стисне \r\nМоя молодість, і життя, і січа.\r\n",
                                    StreetcodeId = 2
                                });

                            await dbContext.SaveChangesAsync();
                        }

                        if (!dbContext.TimelineItems.Any())
                        {
                            dbContext.TimelineItems.AddRange(
                                new TimelineItem
                                {
                                    Date = new DateTime(1831, 1, 1),
                                    Title = "Перші роки в Петербурзі",
                                    Description = "Переїхавши 1831 року з Вільна до Петербурга, поміщик П. Енгельгардт узяв із собою Шевченка, " +
                                "а щоб згодом мати зиск на художніх творах власного «покоєвого художника», підписав контракт й віддав його" +
                                " в науку на чотири роки до живописця В. Ширяєва, у якого й замешкав Тарас до 1838 року.",
                                    StreetcodeId = 1
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(1830, 1, 1),
                                    Title = "Учень Петербурзької академії мистецтв",
                                    Description = "Засвідчивши свою відпускну в петербурзькій Палаті цивільного суду, Шевченко став учнем Академії мистецтв," +
                                        " де його наставником став К. Брюллов. За словами Шевченка: «настала найсвітліша доба його життя, незабутні, золоті дні»" +
                                        " навчання в Академії мистецтв, яким він присвятив у 1856 році автобіографічну повість «Художник».",
                                    StreetcodeId = 1
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(1832, 1, 1),
                                    Title = "Перші роки в Петербурзі",
                                    Description = "Переїхавши 1831 року з Вільна до Петербурга, поміщик П. Енгельгардт узяв із собою Шевченка, " +
                                                "а щоб згодом мати зиск на художніх творах власного «покоєвого художника», підписав контракт й віддав його" +
                                                " в науку на чотири роки до живописця В. Ширяєва, у якого й замешкав Тарас до 1838 року.",
                                    StreetcodeId = 1
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(1833, 1, 1),
                                    Title = "Перші роки в Петербурзі",
                                    Description = "Переїхавши 1831 року з Вільна до Петербурга, поміщик П. Енгельгардт узяв із собою Шевченка, " +
                                                "а щоб згодом мати зиск на художніх творах власного «покоєвого художника», підписав контракт й віддав його" +
                                                " в науку на чотири роки до живописця В. Ширяєва, у якого й замешкав Тарас до 1838 року.",
                                    StreetcodeId = 1
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(1834, 1, 1),
                                    Title = "Перші роки в Петербурзі",
                                    Description = "Переїхавши 1831 року з Вільна до Петербурга, поміщик П. Енгельгардт узяв із собою Шевченка, " +
                                                "а щоб згодом мати зиск на художніх творах власного «покоєвого художника», підписав контракт й віддав його" +
                                                " в науку на чотири роки до живописця В. Ширяєва, у якого й замешкав Тарас до 1838 року.",
                                    StreetcodeId = 1
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(1835, 1, 1),
                                    Title = "Перші роки в Петербурзі",
                                    Description = "Переїхавши 1831 року з Вільна до Петербурга, поміщик П. Енгельгардт узяв із собою Шевченка, " +
                                                "а щоб згодом мати зиск на художніх творах власного «покоєвого художника», підписав контракт й віддав його" +
                                                " в науку на чотири роки до живописця В. Ширяєва, у якого й замешкав Тарас до 1838 року.",
                                    StreetcodeId = 1
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(1836, 1, 1),
                                    Title = "Перші роки в Петербурзі",
                                    Description = "Переїхавши 1831 року з Вільна до Петербурга, поміщик П. Енгельгардт узяв із собою Шевченка, " +
                                                "а щоб згодом мати зиск на художніх творах власного «покоєвого художника», підписав контракт й віддав його" +
                                                " в науку на чотири роки до живописця В. Ширяєва, у якого й замешкав Тарас до 1838 року.",
                                    StreetcodeId = 1
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(1997, 7, 5),
                                    DateViewPattern = DateViewPattern.DateMonthYear,
                                    Title = "Народився",
                                    Description = "Цього дня Роман народився в Києві. В родині активіста руху проти знищення історичної забудови «Збережи старий Київ», добровольця Тараса Ратушного та письменниці, журналістки Світлани Поваляєвої. Зростав та вчився у столиці.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2012, 1, 1),
                                    Title = "Обирає фах",
                                    DateViewPattern = DateViewPattern.Year,
                                    Description = "Коли прийшов час обирати фах, Роман зупиняє свій вибір на юридичному та вступає до Фінансово-правового коледжу в Києві.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2013, 11, 30),
                                    DateViewPattern = DateViewPattern.DateMonthYear,
                                    Title = "Проти несправедливості",
                                    Description = "Роману — 15. Україні — 22. І обох непокоїть несправедливість. Починається Революція гідності. Юний Ратушний — один з перших її учасників в усіх найгарячіших епізодах протистояння. У ніч на 30 листопада його разом з іншими студентами вперше побив «Беркут».",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2013, 12, 30),
                                    DateViewPattern = DateViewPattern.MonthYear,
                                    Title = "«Знаю, що роблю»",
                                    Description = "Під час штурму Євромайдану силовиками Роман хоч і постраждав, але вистояв разом з іншими гідними. «Тато. Знаю, що я роблю», — спокійно відповідає батькові та йде туди, де найгарячіше.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2014, 1, 1),
                                    DateViewPattern = DateViewPattern.Year,
                                    Title = "Боротьба лише починається",
                                    Description = "Найзапекліша фаза Революції гідності у лютому. Силовий тиск проти активістів поновлюється. Роман знову в епіцентрі. Його боротьба тільки починається. У грудні бере активну участь у протестах за кадрові зміни в Міністерстві внутрішніх справ України та пришвидшення розслідувань злочинів, скоєних у 2013–2014 роках на Євромайдані та в Одесі.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2018, 1, 1),
                                    DateViewPattern = DateViewPattern.Year,
                                    Title = "Захистимо Протасів Яр",
                                    Description = "Роман очолює ініціативу «Захистимо Протасів Яр», з 2019 року це однойменна громадська організація. Разом з однодумцями активно виступає за збереження зеленої зони у Протасовому Яру в центрі Києва та проти побудови багатоповерхівок на зелених схилах.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2019, 1, 1),
                                    DateViewPattern = DateViewPattern.Year,
                                    Title = "Погрози",
                                    Description = "Через погрози фізичною розправою та викраденням, про які Роман заявив у жовтні 2019-го, йому доводиться деякий час переховуватися.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2020, 6, 1),
                                    DateViewPattern = DateViewPattern.SeasonYear,
                                    Title = "Перемога в суді",
                                    Description = "Конфлікт та протистояння забудовнику ТОВ «Дайтона Груп» в суді нарешті закінчуються перемогою активістів на чолі з Ратушним. 27 червня 2020 року Київська міська рада повертає земельній ділянці площею 3,25 га у Протасовому Яру статус зелених насаджень.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2020, 12, 30),
                                    DateViewPattern = DateViewPattern.SeasonYear,
                                    Title = "Досвід політика",
                                    Description = "Роман балотується на виборах депутатів Київради від блоку Віталія Кличка, не будучи членом партії «УДАР». На думку членів ГО «Захистимо Протасів Яр», така взаємодія мала б забезпечити представництво в міській раді громадських ініціатив, а не тільки партій. Вибори Роман програє, не подолавши 25-відсоткової виборчої квоти, але набувши певного досвіду політика.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2021, 1, 1),
                                    DateViewPattern = DateViewPattern.Year,
                                    Title = "Домашній арешт",
                                    Description = "Роман активно підтримує виступи проти арештів активістів: одесита Стерненка та затриманих у «справі Шеремета» Антоненка, Дугарь, Кузьменко. Проти нього фабрикують справу. У соцмережах її охрестили «чорний квадрат» через суцільну чорну пляму на відео з камер, в якому нібито побачили Романа. На підставі сфабрикованих доказів висувають підозру в хуліганстві та відправляють під домашній арешт.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2021, 1, 1),
                                    DateViewPattern = DateViewPattern.Year,
                                    Title = "Сфабрикована справа",
                                    Description = "Громадську діяльність під домашнім арештом не полишає. Знімає жартівливе відео про життя з електронним браслетом. Свій арешт пов’язує з власною діяльністю на захист Протасового Яру. «Навіть якби мене на тій акції не було, вони б придумали щось інше», — каже про фабрикування справи. Після подання апеляції адвокати активіста виграли суд — з Ратушного зняли всі обвинувачення.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2022, 2, 24),
                                    DateViewPattern = DateViewPattern.DateMonthYear,
                                    Title = "Підрозділ Протасового",
                                    Description = "Свій Протасів та свій Київ з початком повномасштабного вторгнення Росії Роман добровольцем захищає у лавах Збройних сил України в підрозділі «Протасового Яру». Спершу була Київщина, потім — Сумщина, де він брав участь у деокупації населених пунктів області, зокрема Тростянця.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2022, 3, 1),
                                    DateViewPattern = DateViewPattern.SeasonYear,
                                    Title = "Холодний Яр",
                                    Description = "На початку квітня вступає до розвідувального взводу 2-го мотопіхотного батальйону 93-ї окремої механізованої бригади ЗСУ «Холодний Яр». Цей батальйон обороняв українську землю в Харківській області, зокрема в районі Ізюму.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2022, 6, 9),
                                    DateViewPattern = DateViewPattern.DateMonthYear,
                                    Title = "Завжди 24",
                                    Description = "Роман Ратушний не дожив трохи менше місяця до своїх 25 років. 9 червня 2022-го під Ізюмом на Харківщині він загинув, потрапивши у ворожу засідку. До кінця на бойовому завданні, вистежуючи ворожий танк. Тіло Романа декілька днів було на непідконтрольній території, доки його командир з позивним Боб зміг його забрати. Чекав сильної грози, щоб не бути поміченим ворогом.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2022, 6, 18),
                                    DateViewPattern = DateViewPattern.DateMonthYear,
                                    Title = "Байкове. Вічність",
                                    Description = "Романа Ратушного поховали на Байковому кладовищі в Києві. Попрощатися прийшли сотні людей. Батьки, родичі, військові, знайомі та друзі Романа, громадяни, активісти, представники влади. Прощалися з героєм у Михайлівському соборі та на Майдані. Перед похороном над труною з його тілом розгорнули прапор України.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2022, 9, 8),
                                    DateViewPattern = DateViewPattern.DateMonthYear,
                                    Title = "Вулиця Ратушного",
                                    Description = "На засіданні Київради одноголосно підтримали перейменування вулиці Волгоградської у Солом'янському районі столиці на вулицю Романа Ратушного. Таку пропозицію подав письменник Євген Лір.",
                                    StreetcodeId = 2
                                },
                                new TimelineItem
                                {
                                    Date = new DateTime(2022, 9, 13),
                                    DateViewPattern = DateViewPattern.DateMonthYear,
                                    Title = "За мужність",
                                    Description = "Романа Ратушного посмертно нагородили орденом «За мужність» III ступеня — за особисту мужність і самовіддані дії, виявлені у захисті державного суверенітету та територіальної цілісності України, вірність військовій присязі.",
                                    StreetcodeId = 2
                                });

                            await dbContext.SaveChangesAsync();
                        }

                        if (!dbContext.TransactionLinks.Any())
                        {
                            dbContext.TransactionLinks.AddRange(
                                new TransactionLink
                                {
                                    Url = "https://streetcode/1",
                                    StreetcodeId = 1
                                },
                                new TransactionLink
                                {
                                    Url = "https://streetcode/2",
                                    StreetcodeId = 2
                                });

                            await dbContext.SaveChangesAsync();
                        }

                        if (!dbContext.Facts.Any())
                        {
                            dbContext.Facts.AddRange(
                                new Fact
                                {
                                    Title = "Викуп з кріпацтва",
                                    FactContent = "Навесні 1838-го Карл Брюллов і Василь Жуковський вирішили викупити молодого поета з кріпацтва. " +
                                    "Енгельгардт погодився відпустити кріпака за великі гроші — 2500 рублів. Щоб здобути такі гроші, Карл Брюллов" +
                                    " намалював портрет Василя Жуковського — вихователя спадкоємця престолу Олександра Миколайовича, і портрет розіграли" +
                                    " в лотереї, у якій взяла участь імператорська родина. Лотерея відбулася 4 травня 1838 року," +
                                    " а 7 травня Шевченкові видали відпускну.",

                                    StreetcodeId = 1,
                                },
                                new Fact
                                {
                                    Title = "Перший Кобзар",
                                    FactContent = " Ознайомившись випадково з рукописними творами Шевченка й вражений ними, П. Мартос виявив до них великий інтерес." +
                                        " Він порадився із Є. Гребінкою і запропонував Шевченку видати їх окремою книжкою, яку згодом назвали «Кобзарем».",

                                    StreetcodeId = 1,
                                },
                                new Fact
                                {
                                    Title = "Премія Романа Ратушного",
                                    FactContent = "Український журналіст, публіцист і письменник Вахтанг Кіпіані від імені «Історичної правди» ініціював заснування іменної премії Романа Ратушного для молодих авторів за публікації, що стосуються історії Києва. Гроші на започаткування премії дали батьки Романа.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Стипендія для активістів",
                                    FactContent = "На честь Романа в Інституті права Київського національного університету імені Тараса Шевченка заснували стипендіальну програму для громадських активістів, які здобувають юридичну освіту.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Премія Романа Ратушного",
                                    FactContent = "Український журналіст, публіцист і письменник Вахтанг Кіпіані від імені «Історичної правди» ініціював заснування іменної премії Романа Ратушного для молодих авторів за публікації, що стосуються історії Києва. Гроші на започаткування премії дали батьки Романа.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Карта мафіозних зв’язків",
                                    FactContent = "Романа можна вважати «хрещеним» Державного бюро розслідувань. У 2015 році він самостійно створив карту зв’язків російської та української мафій, засновану на відкритих даних. Підтримував розслідування злочинів. За його даними, таких взаємопов’язаних осіб було близько тисячі.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Стати громадянином",
                                    FactContent = "За словами мами Романа, Світлани Поваляєвої, маленький громадянин Ратушний почав ходити на мітинги та протести із семи років. Першою суспільно корисною активністю стала Помаранчева революція. «Участь у політичних і соціальних процесах своєї держави має бути атрибутом життя кожного громадянина», — наголошував Роман.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "«У мене все добре»",
                                    FactContent = "Якось під час найзапеклішого протистояння на Євромайдані він подзвонив батькові та звично сказав: «У мене все добре, ми з друзями вже їдемо з Майдану додому, не хвилюйся і на добраніч». А через деякий час в той же вечір Роман вже коментував події у прямій телевізійній трансляції: «Ми зараз штурмуємо Український Дім, там засіли внутрішні війська, їх біля сотні, але ми їх зараз звідти викуримо…». Він завжди був там, де найгарячіше. Каску, в якій Роман був на Майдані, його мама Світлана Поваляєва згодом передала в Музей Революції Гідності.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Життєві плани",
                                    FactContent = "Після Революції гідності в 2014 році під час подорожі Європою тато Романа делікатно просував йому, юнакові, обпаленому Майданом, ідею навчання в одному з європейських університетів. А Роман делікатно відмовився. «На цей момент мій життєвий план такого не передбачає», — відповів.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Культура життя",
                                    FactContent = "Роман зростав у середовищі культурних діячів Києва. Це не могло не позначитися на його особистості, поглядах і смаках. Театри, вистави, виставки. Багато музики та читання. Цікавість до історії та права. Разом із братом Василем навчався грі на трубі в Джазовій академії. Відвідував концерти в Національній філармонії та Будинку органної і камерної музики. Друзі відзначали витончений смак Романа в одязі, але в цілому аскетичний підхід до матеріальних принад життя.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Громада понад усе",
                                    FactContent = "Всі знали Ратушного як щедру та безкорисливу людину. Так, компенсацію Європейського суду з прав людини, яку він отримав як потерпілий від побиття студентів «Беркутом», Роман фактично повністю витратив на громаду та боротьбу за Протасів Яр.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Сенека",
                                    FactContent = "На фронті Роман взяв собі позивний Сенека. Йому відгукувалися погляди давньоримського філософа на жертовність та героїзм заради суспільства. Сенека виклав їх у своїх «Листах». А Роман підтвердив свої переконання яскравим життям із героїчним запалом. Зі світоглядом Сенеки погляди Романа порівняв його батько в своєму тексті пам’яті про сина. Спецпідрозділ радіоелектронної розвідки і радіоелектронного штурму 93-ї окремої механізованої бригади ЗСУ «Холодний Яр», де служив Ратушний, назвали «Сенека». На його честь, бо він його і задумав.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Заповіт нам",
                                    FactContent = "20 травня 2022 року, незадовго до загибелі, Роман на своїй сторінці у фейсбуці опублікував пост — свого роду заповіт нам. «Допоки Збройні сили вбивають русню на фронті, ви нездатні вбити русню в собі. Просто запам’ятайте: чим більше росіян ми вб’ємо зараз, тим менше росіян доведеться вбивати нашим дітям. Ця війна триває більше трьох сотень років…».",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "«Загинув за Тебе»",
                                    FactContent = "Побратими стверджують, що Роман готувався до свого останнього бойового завдання. Дав вказівку зібрати свої речі та віддати їх братові у випадку загибелі. Написав заповіт з докладними інструкціями. Описав, як саме хотів провести свій похорон. А ще написав у заповіті кілька останніх слів про любов до свого Києва: «Загинув далеко від Тебе, Києве, але загинув за Тебе…».",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Мріяв про вільну Україну",
                                    FactContent = "У липні 2022 року в Лугано президентка Єврокомісії Урсула фон дер Ляєн під час конференції з відбудови України розпочала свій виступ словами про Романа, активіста та журналіста, який мріяв про Україну, вільну від корупції, та поклав життя за її суверенітет.",

                                    StreetcodeId = 2
                                },
                                new Fact
                                {
                                    Title = "Жива справа",
                                    FactContent = "За словами мами Романа Світлани Поваляєвої, він заповів фінансово підтримати музей Шевченка, Національну капелу бандуристів імені Майбороди, а також видання «Історична правда» та «Новинарня». А ще попросив донатити на добровольчий медичний батальйон «Госпітальєри» та інші волонтерські організації, що займаються екіпіруванням ЗСУ.",

                                    StreetcodeId = 2
                                });
                            await dbContext.SaveChangesAsync();
                            dbContext.ImageDetailses.AddRange(new[]
                            {
                                     new ImageDetails()
                                     {
                                         Alt = "Additional inforamtaion for  wow-fact photo 1"
                                     },
                                     new ImageDetails()
                                     {
                                         Alt = "Additional inforamtaion for  wow-fact photo 2"
                                     },
                                     new ImageDetails()
                                     {
                                         Alt = "Additional inforamtaion for  wow-fact photo 3"
                                     },
                                     new ImageDetails()
                                     {
                                         Alt = "Additional inforamtaion for  wow-fact photo 3"
                                     },
                            });
                        }

                        if (!dbContext.SourceLinks.Any())
                        {
                            dbContext.SourceLinks.AddRange(
                                new SourceLinkCategory
                                {
                                    Title = "Книги",

                                },
                                new SourceLinkCategory
                                {
                                    Title = "Фільми",

                                },
                                new SourceLinkCategory
                                {
                                    Title = "Цитати",

                                });

                            await dbContext.SaveChangesAsync();

                            if (!dbContext.StreetcodeCategoryContent.Any())
                            {
                                dbContext.StreetcodeCategoryContent.AddRange(
                                    new StreetcodeCategoryContent
                                    {
                                        Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                                        SourceLinkCategoryId = 1,
                                        StreetcodeId = 2
                                    },
                                    new StreetcodeCategoryContent
                                    {
                                        Text = "Хроніки про Т. Г. Шевченко",
                                        SourceLinkCategoryId = 2,
                                        StreetcodeId = 2
                                    },
                                    new StreetcodeCategoryContent
                                    {
                                        Text = "Цитати про Шевченка",
                                        SourceLinkCategoryId = 3,
                                        StreetcodeId = 2
                                    },
                                    new StreetcodeCategoryContent
                                    {
                                        Text = "Пряма мова",
                                        SourceLinkCategoryId = 3,
                                        StreetcodeId = 1
                                    });

                                await dbContext.SaveChangesAsync();
                            }
                        }

                        if (!dbContext.RelatedFigures.Any())
                        {
                            dbContext.RelatedFigures.AddRange(
                                new RelatedFigure
                                {
                                    ObserverId = 2,
                                    TargetId = 1
                                },
                                new RelatedFigure
                                {
                                    ObserverId = 1,
                                    TargetId = 2
                                });

                            await dbContext.SaveChangesAsync();
                        }

                        if (!dbContext.StreetcodeImages.Any())
                        {
                            dbContext.StreetcodeImages.AddRange(
                                new StreetcodeImage
                                {
                                    StreetcodeId = 1,
                                    ImageId = 1,
                                });

                            await dbContext.SaveChangesAsync();
                        }

                        if (!dbContext.Tags.Any())
                        {
                            dbContext.Tags.AddRange(
                                new Tag
                                {
                                    Title = "writer"
                                },
                                new Tag
                                {
                                    Title = "artist"
                                },
                                new Tag
                                {
                                    Title = "composer"
                                },
                                new Tag
                                {
                                    Title = "victory"
                                },
                                new Tag
                                {
                                    Title = "Наукова школа"
                                },
                                new Tag
                                {
                                    Title = "Історія"
                                },
                                new Tag
                                {
                                    Title = "Політика"
                                },
                                new Tag
                                {
                                    Title = "Активіст",
                                },
                                new Tag
                                {
                                    Title = "Борці за незалежність",
                                },
                                new Tag
                                {
                                    Title = "Герої",
                                });

                            await dbContext.SaveChangesAsync();

                            if (!dbContext.StreetcodeTagIndices.Any())
                            {
                                dbContext.StreetcodeTagIndices.AddRange(
                                    new StreetcodeTagIndex
                                    {
                                        TagId = 1,
                                        StreetcodeId = 1,
                                        IsVisible = true,
                                    },
                                    new StreetcodeTagIndex
                                    {
                                        TagId = 1,
                                        StreetcodeId = 2,
                                        IsVisible = true,
                                    },
                                    new StreetcodeTagIndex
                                    {
                                        TagId = 2,
                                        StreetcodeId = 1,
                                        IsVisible = true,
                                    },
                                    new StreetcodeTagIndex
                                    {
                                        TagId = 4,
                                        StreetcodeId = 2,
                                        IsVisible = true,
                                    },
                                    new StreetcodeTagIndex
                                    {
                                        TagId = 7,
                                        StreetcodeId = 2,
                                        IsVisible = true,
                                    },
                                    new StreetcodeTagIndex
                                    {
                                        TagId = 8,
                                        StreetcodeId = 2,
                                        IsVisible = true,
                                    },
                                    new StreetcodeTagIndex
                                    {
                                        TagId = 9,
                                        StreetcodeId = 2,
                                        IsVisible = true,
                                    },
                                    new StreetcodeTagIndex
                                    {
                                        TagId = 10,
                                        StreetcodeId = 2,
                                        IsVisible = true,
                                    });

                                await dbContext.SaveChangesAsync();
                            }
                        }
                    }
                }

                if (!dbContext.InfoBlocks.Any())
                {
                    dbContext.InfoBlocks.AddRange(
                        new InfoBlock
                        {
                            AuthorShipId = 2,
                            ArticleId = 2,
                            TermId = 1,
                            VideoURL = "https://www.youtube.com/watch?v=LDwDUIjb93Q"
                        },
                        new InfoBlock
                        {
                            AuthorShipId = 2,
                            ArticleId = 2,
                            TermId = 1,
                            VideoURL = "https://www.youtube.com/watch?v=SDxjdfr6qnc&t=3084s"
                        });

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.ImageMain.Any())
                {
                    dbContext.ImageMain.AddRange(
                        new ImageMain
                        {
                            MimeType = "image/jpeg",
                            Base64 = "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",
                            BlobName = "okhbqu+U3+ErGdOK0RA7V5yF80BaDOh+hBadZ0zMI3w=.jpeg"
                        },
                        new ImageMain
                        {
                            MimeType = "image/jpeg",
                            Base64 = "iVBORw0KGgoAAAANSUhEUgAAAYQAAAD6CAYAAACh4jDWAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAQvXSURBVHgB7L3Js23JVeZ5bsRLepDo1NDpBWBGZ3SGIQMmRE2ZZA5gDDVlUlkTpoi/IKsmTEtMYUDmAMzKDAxhDOgN0YM6JISQRC/6RoqX7zsRv5e/+N7n5z7R5Cjc7N5zzt6+3ZcvX/6txpt9d/nfkB49evRNjz/y942P/x6+8v3yyvfX0mvptfRaei29nP7q8d/7X/l85+O/D+Tz7u7uHZf/Denu8h+QHiuA1z/++C+P/77zlc/XX15Lr6XX0mvptfSvTVEQ73j89z/y+VhBvP/yH5D+XRXCY0Xw4uOP//z47/surymB19Jr6bX0WvqPSm9//Pcj/96ew7+LQnhFEfzg478XL59k+qd/+qfr3+Myrn+vlPeqv5deemnV+SQ/6TFznsrX1/KbP/9e5ff3pu/fOzVdqcO/nW8927+fe+65y7PUt8pL6uebBv7gx62yuh1u26mOlU7PdRnPIhtd7rOU86xl+FnL7/PPP/8qWixTyfeJT3zi8s///M/X7/lb9T8rTfflW3T8W9Ot/oEPLdt+9t8j/Ue0619b10nGO+9nfuZnXj7jMz7j8q9ICSv9v4/LfPvl3yH9mxTC40Y9fPzx3y4vh4WO6a//+q8vH/zgBy9/+qd/evmbv/mby5//+Z9fr3kQ5O/jH//4VTn84z/+43VQ/P3f//31L9/Jk788k7z5Y9BkoF0b9AoQ5nf+/tN/+k9PBmE+P/VTP/XJH3m4l+fowH/5l3958gddoSMpNOR3Pi3Y/b2B0uVTJ/WnHbn/KZ/yKddP2pDPBw8eXP9oYz6Tn+eT8pzLTbs//dM//Vp/ykh70w7KpUz+kj/3Um7KyDWXmfv8JVF+nkGhu6zw5tM+7dOe0Er7zQPaR3I/AB7wx8YBbSBRpnntQec63U9teJD3dJ9+SuIz19NWruU79SGnltsM+s/5nM95wg/kOHL2d3/3d5ePfexjlz/4gz+4vOc977l8+MMfvl53fdSB3MMrrsPHbheJvnBb/QwybR74OZ5p5ce4oFx4w3PwwzST55TuU4amw2OsedDKteWQe3wuWaWv0kbLqXlAm5KPaxkLvkfZNhJaNr/gC77g8oVf+IWXhw8fXt7ylrdcP/N3T/rvj//+739rKOnB5V+ZHhP+fz3+eNvlEBr60Ic+dPmjP/qjy2//9m9fhRwGMJgNnu4wmI2AZ0DQCTATwTslgJ+y80zAKQBmBdGdnzwBtKSAXFLqjyLIX66ls3m+NX3uQf8rPHpSv/Na+bTlnMSgDK0oAgsnCjL0djkoCsoJTSkn1/Mc7aRsQADhpR1J4WEPIgY0lix1WZnxPOAILwwQtMm8oo7mh7/7ufYqDY62xPmk78jfwNmKw88b9FaeBmPT5N//8A//cOVreA+9GDT5+6zP+qzLl3zJl1xlLXljOEUGG9j4bK/VbWp6DEo80zJneXCersdltfJ1srHTfOsybZA4rxUJaSk7JwMuz1kZ+nljT5LHt2VgKTrTY+D3b/fzMk5MY37/yZ/8yfUv2Mm1N7zhDZev+7qvu3znd37n9XOkGOX/5XHetz0u/4cu/8r0SXsIj16eMP7xyyE8FEXwC7/wC1dlkATjLLBJDBTAzQKfwfCXf/mXl7/927+9Wk0GGFtIqyMB0ICTLey4ZFEIeSYDLJ+5HouN52K5oRAoP95KvJr8hZ54LND7Cj+etB0ApG2U4QGBFW1L1yAIWEI/wGGlkmdzP/eW5xFgz33ypu35w6PKPZSf6+Q3SuR1r3vdE9ryTP6wWKkX+vJ8ykzK86mbZ6APa4nnoSXXsb5od1umTwRW9S5Q8KAD2OjT/KaNAB8DmOuEa8yXVsZJeEUBd2QW+kjuf/oi/Mszn/3Zn/2k/tSR61EA+UsKILz73e++fOQjH7n8xV/8xZO8yC50cx1FvIDW463BqAHeyWW7T1ZyXsq2VW4Pft2HRl+zgmsl0YrPZVhJd/v7rz1YG1TNy6UAuv6l0Fpp+v4qtz0tKyc8iO/+7u++vPjii5dDev/jv//jX+MtfFIewqOXl49GGTzse1EEv/RLv3RVBG5kW1ntFbgzaDAuNOCD8AA0pLaObWEmXwY5YYv8ZtAmEf7AWutOBFABCFuwACv1uL125xkgVhDLOmg+ua0IKIPyrjwbl4WAE6IA0JLSDiuUZUnnN3zxgLN3gdIzUCLA6S/qI3S0vMHkIxzFNXhqnpk3BnAGcfc/gHh393TIpAeljQr6f1nQgAp12sAhLyG6lgnTbsAlBMl3gw4GT+T2zW9+8xN5+6u/+qtXyQ/toR1u37N4BabJXsGSq5Yxync++sJWvutvvqx+cahlfa5yVrsaqLu/DLS02/QvuekQD2W0B3xXxm97TCcaF2a6Xf4eY+GHf/iHLz/2Yz92+Z7v+Z6lGB4+/vuZx/n/z7tPctL5mRXCo5cnjqMMXhUiikv7cz/3c5f3ve99TwaytXxrQ661RZFBRTw1Vjlusp9dwmwhtvUYwOlBnudQAoCVY9HQQR0G967fwJ3kuLGFxQJl4TFQ+Lr4ff0E3O0NWMGYB5QLL/E0oBPPhOTYN+UmD1Yvf4SeOqQDTfx2H2D5uy6H7+Cn52GsiKCTOqGVZxyrt5JG0TQQWQ5dp5WalU2DJXJghWjewVsrcvOlvdnIN3THC7YRkbLj0WEUxWtIvRlreCiXywZVy41/uz39fN+zglnebvPV7fQ1jAaume8LEBucF9h3G6jbaeHP8oCcPKY93nuer9u8+NTtpHxoagNxKaGmzZ/cz5xsFMPP/uzPXr7/+7//Ou+g9PDyslLIvML/c3nG9EwK4XGh33t5eZnTq1KUwE//9E9fAdyNaOGkMd1JfgZGRuAdlnB+M9mWZwuRB34SIRTCG+5wl0G5gGja1ZZg7jG30VaIhR8B8nyHreruXNNgsKSdgA3gQ/uwcHv1Cnmp3yEWC+3JQjaNHgS0sa0519kWZgu9wX4Jf4fe2oVuAPOn+duAZ6/CbWqjoel5NIwbK8YGYj9rulYb4U3CkYTq8hel+frXv/5KWxRGlEK+sxgDJd1jo/vvZLis9vFsX/dY6Tb4uZZ/09KKftXbPDUPG18WnaS25qHJ8sczVloOIbqdALfHoj1Fe/D2knn+BPA2Bn19yVv3GSnP/87v/M7lB37gB67ewnd913ddKv23x/lfd/eM8wr3KoRHL3sGb+/rv/zLv3z9O02mmZHLbTWII7CsLkIhJBkMAlxYgnRAh1NI1NkgTMgi37GEocWKKPVk8MVVz4DM3EHos+XVwIbQYMGSejAgWK3ELDw9CN1uhJB8ju8aqFFc5OM3gpvywgPoRjFAOzF9aHYfwmPPI7jP7VVQPtZz+tiT5QbT5oH54oHWBgJeIeVhfTcIcG/NU1iB4im4Pl83jS3z7Qm6jShTJuQxPqIQ8AYIc2aeIc9H7iKL+TNo2nvyYoclR/3diTHYxorBr8cwyeW1ZWxAg8+tFBbQt0HSeQ2YbYDZiu9+cJv8vGXX49IGnfHF/XxSrkuR+bfbZgVluvqZLtdlBaPe/va3X72G7/3e771UetvjPB+7ewZP4aZCePTystIf7+vveMc7Lr/3e7/3Kq3ZGtgTOm6kG4srDVAxd8CAIYTkMt3pSUxcAkrUy2/CEL7OKqRc9yRhUvIzkc2SV2LErHhqV69DQ/w1yHr5IUszAdQGRYdBGgTcPgtl2gDYUT5ehmPVHdKibCa8uddx1ZSf3/COeyjmLt/LXD1Y4Eevckqy5eVrpsVWle+jjEkO/3jQk8yHZanZaCF5TsFyRd+3YUC7XRZKEZlDhgP4AX8MpCiHrDp605vedF2qndgxk+/QzVxXA6L5ZzqXvFIWyupklHgsN2j3+D6FaNoLbX4vo84A7XsN0Kvf2kvPNUKZtM08teFjz8FK3bLo8d70k1pJ9AR9K6lWpo2jqw7ST/zET1xXJ73tbW/rfQ3xFO49AuOoEF5RBj9zqTmDxKuiDFoDulOsKDrm6OcQ/AbZtjZtqSSlQ9siJvSR5EFMrDYgllVGyeN4NmAE4GG1Av7Q59UdBm3TdwIAC08rz26n6W9wtNWzYurmcytk2onyRFEyAFAmLewONbif+Q7v8SRQVE0LbeE+/DRoeYBBC6DuuvyMgQ2lh9JvuaEs2mSL2nMD7pMFRgbWTuYTZdmboy+R4fA9vIt8xjNAXqMYCBVl9dsb3/jGq8LIiiPvj3EbSJaz7rPmofkDT/m0HDeodVrXF6CZP/1pJdPlrfyug7ZQh/O10rKnsurptjc+uV0tAw3WDeZ8mv/uK+dt2m+V7Xwf+MAHLj/0Qz90+cEf/MFWCj/+ON83391YfXRrK2s2nD30hV/8xV+8bphp8PMAwlp0KMfAaCYAZF5h4kHcCsEdkEEUgMfKZ0MbZbJ3gMnUTNKxLDL3WeLnCUPu2aJMGRmYcekpP8nLRZtmeNJewbJ6CWEtC6EFu61MeMdKKJLnYsxryu825rv3WQA4aS8WKyD2vPaIUHeeA8gaPKF9haRIDU49iPCiaJv51UCyBijXun8aeAwoVo7O634xDVbAvteyb15Y+USWM2eQAWxZTMgyYQD2JySM5OeQceSs23MrLu5+sPx5vDVgGpQs981TA1SHRvvZDsM2H90+rrsOK+j26NriTj7k255WKy+PV3vBBnXnJ8/dwcJvGhbAdx8172+1vdP73//+q1JIlEPpumXg0ctbB2Z6/kBMNp39V1/LktJf/dVfnSsFPHFpECO1ELkhVgqAOgCDMFsYDMIMLq9v92CJ0sgAIhYbYCcE5LqhPVaaV8eEFsJG7JdogKCckwvbA+xVzNdKFXjXwtwhDpSIlUavlDIdhIE6dOOwDvQBvPQx4QN2MS8PxcmT2xgHPANPXQ80mrYeTG2Zub+430BumSSheF2fvRnXZ/nq7ydZbmBs5e3r5gP32fkduiNztDtGS7wCxkfCRtkTwzPmT4NDg0ePHcuLZcZtNUA2uC+ZMw+dFpC1AWBl3oBLvy2wtwfo8J/p57cNTQO9yzOGLb6sNpGvPSCSaW8Zuhvegmm5ewbvwHn4jDGR/Vxvfetb/eibHv992mNl8f9fRnoqZPTo5VDR23zt93//96/KYOR91V8GXGvAtgBI3POzjpkzgdwx0w4huDyYSkgkA4xJO4M/yoZBlb94EHbjUQZYzQgdZdkaPgkK+RfoIHTwCU+mFZvBGqDjOe92NQhTly1jf+eTtli559qpDNrqiVV7g91/5lH4aKVgZeF2WcG1fKFYzNu2xGlD94N5bdBwGSgN73L3xHdbr5ZJ2sm8CflNk/uhl7eSqM/8jiy+973vvdLGaiQbG7cAva1SK8sGXSsvg2n3g+Xcsrf6y9ddl/O4vDYIHNY1zU49vpax4jaSf3kUNnzWOF789v2OBnhMmq8n5XirnW7LUljrmcz3vvDCC7366L8+zv8/7sZ8wppD+MGL5g1ijUQZGIi7IavDSW05kcxwM9FLIGMdYaW681p7GsjYhUusPECEkgB4WWnDxjV2yqY+QiA+R6lDAwhoW+YkDxADvTubMpbHYz4ZsOGRBc47gS1YlIOi5dgOQM/7MHg+bc0zyWtLn2Q+e3A4b8/R5LfnC07tuwqjVh5ZljoMxMDzBK+fsXx4oJ0Gj/uXpZ9dXsu1eeJye17CSt1ABW1sGHQ7cy3Az2bAKKjsWP6zP/uzJ/LcitFy0da8lUCPQ7ejgamBuWVhKYdlCVvpnYDPeXzPeV1v97P5wByR0y1Leym8VnLkdTvaAHPd3afuK49n8lqGT2kpXtfV/UH60R/90cvXfu3XXh4+fOjigvPv6DpepRAevewdfJ+v/cqv/Mr1LKITI00o1/mkg7rDlntti9dC4sOhXJ4tsSRWDrHpzAqHwegwAUckpGzmEzxHgOJot5a0BkjSsr6sQFpQPBDMjx4IXYfpARwAmRZceEGdnM/UB+a14DdItIttkElCwb5UIRzoM1gZPNaAb35hffcA9fMeYH29B6GtNiu3PhID5d0rVdpTNd1ePeYDGFvJWNl78QMrxCKP8BGwsQfn+nuuwH3gsbhA4wSwfc/PJNlYa8t7gahpgY9WuFZeqz7a1f3cY32F6lxW37OBxbWTbDot3LNcnZ7p67dk6PSMn22c6DYmVP4jP/Ij10lmpRcf3/u+uzoltT2E/88/MjGRFUULEJvZ69qtRi9lwn2vouiliYC6O455BC89xSuwkkk+PAImJz1nAXOtDKjHa+qfxVpqXi3htyXn5yzoLezXTnul/YAkQHaRMHjiElrdBrcxv717u/cV4Dn0gYG0wYPKYNihE3svjuNbaVM2yq3DOz1w4RtKsucWCDl6vsB0UgZ1obwM1gBx9wtlG+zdhrViyfUlRR5zj/ktjBq8yhgqjIVWcpbP5k1PGNsLskxafhtge9WcZdN/bqPztMLssdG0Q6Ofg9cOpXV5rew8PnvsLCPL92iHw8InEDfPTgrByeX4OfOafB2yWumW8up2ZinqT/7kT3boKBri7b7wnAp/eKkD637+53/+KTemO8p/PWAsLGagO8zPEc4gVNNChQXq0zpZsucVMAzgDiswQFN2QmGZdIlrnpQyM48AAHhlg5fGwuwFRtxzZ3kwmX+9umkpmhZEW+rtmhsI0r4oOlvU8NW7yt2GdaaOQd59aNDz6bG5hnIlodwNqFaIKGKvTrPC6h3hrbA8aHrwNkg038xXA4OvOdFHnv9Y9fXE+hofpsllhLfsS2CCmRNPWyFYFk1r09Syt1K337LQMk7yQYEnT9ry0oq16+kyOp/nEpunq62r/0g2Dpp3rUTWWDa9xobF4+Zlg7Xb2m13G/vPxtKpTtOb0FGtOnr46OWNx0+SPYQf9I13vetdT1YzuGMa3Dr2Z0IMFIsp5HcHAxLE4hhgHD/BJitbDhZY6ENZ4AnAWCwxrF7H9zly2McNe0KZwX1aJmeBPvHLSpHk8g28Lt/eDyul8AAAYnsULAlt4bFlTxjNezncpzzrFUEk5iFoB4rB7VwDElrs+ZlPDtkZgFfIA5oNsL2SqD0MA2oPPpdtwLCXxbX2aFpp0VfIYlvplhHLIrvn8VTYwc/7Nxr8ur8o1+OCfOZzj1V47/b4WWOA+WDPyOVaEaz06IYi77FjWkgeQw3Y/nR9zG85tWHh53muael2uN3tiSy+dbJB3AbCSXnaoF70mAZSsC8b13LEhVJw/x38ePBKIbwD+UnKyiIzj8734GmL4NHBomorYjGoBy7J4QWft7/CKcwjsHIIILOFz8F5Xq7KoHMIoAV5TfoACubP4kHzB5DxtSToMuCYLwx06gQoKcdgYEDkE56gdE0jwmjgxWJ3yCd1G6yerz0n9GWS6adeh2KsIFourPDbKmsZMe0ofysD+rQn6Gl7D8L+DQ3LU2rwAHjoT7eh5cX9x2q4POv3diDLHULwPM4CM/gBvUs2nVx2j3sDP3lulecx1IB6K62w08IXK51lORvgu43wvENCNuq6zkX7SVGY38aMlw4hK7cdDGul1O1ddKyyzR/GSMJGpRBeDP4/zpN3Nj8JGUUZPFlZlDBKVjXYhXcFbjjXF2HNWAsTIOHy8sdGG/JZ4GF0wjvEWT0ICfvkLynWPscD0A4GmecSUBIwz+4xdNri6linheHUYa0YCEXRboeCThYp/PeKKf6oY4GtLU1WUrle08S8ildqUC/9g+CmbN48R4jHAGpvADoMVkme52ieA4pMgvdKGZdno4DyffQJfZ9khWpe21JGplx2W2hWhu5rGykOObYce16MMpI3yoGD7VAqKPSWiwYeKw7ueSWM6WvZch74aoPORp3DiW6H8zU/FkgvekyX5d70rHKdp8u1RX3Luj6BrmXTdPZ4a3nwGLvV1iSPG+NiGx796bKbtlZ6wXZevKP0ZM8ZIaP/7Lu/9Vu/9VSjmshHj86xMJ6ziwsze0C0xeUdhIAfA5zymCy2MDz//KtPLWQVTa6xlwBQs8BaAQGc1N+d6fYu13CBhvPa0jcQL6vo0bAQvFTV4GVlh8KjXrfFQEhdPrvGwtd9Cu2c/cSWeMJvHgw9SNxOLF7P7+D5uX1WjkwKdwjyxAv6EZoN/JYT89v9Sh226j0OLMuWOa730e2e0+K6ZdDAx7LXHFcRT6EnlMMHZNZ0L0PNsuN6V//SR6uN3OOaf3fdbktjxK1r8NK0UPYJ+LqMlcd0d/sdbjylUzjX9bQiOgF207ba6T/3GXn6+wmHV/n07c/8zM/0W9e+ky8ohBd9N+905eGe/LQALwFrQbGlyH0GjC04P4tF47dYxRL9/M///Otgye4704Xgeu4h97Ldn7ghlhb0+jA9K4dWGPZWehmk27k6oDvRfPTEpN3KztsdapCzwACa8Nv8sfXiFVgNNICa5wPauiHUxHxOvAnCHO3ZWF6shCw/lhO+M3gdSnIojf7wJ8lzPh5Y9KNXIcFH2gq9Drc0wPFnhW4FhELIfRSz+8NjpgGXcgnF5Y11oZf3JdhwaYVCcp83zU7m/y3wJVlWXZ+NtW4L311mK2DX7zyn352329RYtBQhn22wPoviSWKcLY94GYo9jhc/LNdLsbSy/mRpdt5sJcjkss45evHRK2GjB49efgvaq8JFmUymMIdIsD6xXAygTgBXx8lt3WGJ+0XuAZeUH0INaEwQ25JkEpX6YBixQcpg4NlyxMp1uMVtNRhZiXggmOEG3R4AJwH2M33dHd/54RXtIrUX5TiwN6HZmqZPDTZe7WNe0w/U2evmUSiAWY4LsQKBbujjBUZ4KbSBepk4Nzjbym9+AMA9SQ3vbCHSz+7PLjvJ4Eq4xqEryvZkLO2wZUr5pDYmLAe0NzxP6DNKIQYQIc2E/FohLM/DZbs9bld7A24Tn8v487M9JhYAvfTS/SeBur7mSYNsP298abkwgDZtxqNFt+lzWNbHrH+ilvdyrYG9abeMuJ7mZ3tkzQvkbinRU5tiYMToLy/hxcd//z3m0jf56oc//OGnGgtARqiJ03M8dATX70VmUJlBLay2RAEGdyTPEz5is1OOAAZkGqBdvwcVIIY3gLsNOGHZfWIsHWst7AFmRWiANqA4LevEvG2rwVZ9D8a0AyvFIRorRANRDwwLGfT6PdOU4fhw/jhMEEXqN5x5mXA+HVLq+SKHlgy6gH4+USSWRQP2svjgB9dsbWPMsFpnAajnC9w/rOiyfKxYtMNGyL/LsFy01w1fkpjT4lytBgW337Jo48Vyt57zMy0/0NzP21syPZ1O/eP7DcaWccu/f3fbSA24lge3o8f3ynsCUtPWCq7xoPndfd9tIc8thel+S7KxfWrv4jnXM4/wTAohB2k1Me3CJLEqIn9+Mbk7EFBpAgEID9KUYQXjepj4dVjEwGpPIkDEJKe9kZSJdeXQiWmz9WzLiA54riarWim0ldXWhwcWqa0hX+uYKr/tyVi47AUBYqbLis20UF4UL0cjEI/3cdVNJyBp0KV8+M/mQudH0bictt49n2CL1qC9+sDgbh66TPqf9jssBa/NK2hyeLP7zYqKeuGBj2Xv8i0jHLWSP14ziuJlT0a/l6ONsAbXy2WHFxpAl9dkml1Ot6FTg2grXJdhOcSQob8chjOv3edWgi2fi+au+6RITrzDY+5nOqT56KD8oXnxefHTz1pWjU32EnqM26BzyhHZlR7mXyT1G30156WsRAdxEiMg4fXRrnS57tcKX5kMJn/ycXQEk7+tFYn3o0hQGHSAQ1QwBjDjedZ6u7MtuAYfW+fuLIMD1xsgoHlp6Nbm8JXnPPi4tiYPm074bWvfVof5iZJeysX7GsIv3j/tMBF89JLd3PcKGHsZLADotvLpsBACnATvGxRsdS93n99WCNSNt9KDCTr8vPuIcAEGiQd0h4lamdEfSxlZ8bfBYSXtMbNWWpFMQ+c5WY8tswtQHW5r0Fxy33W1IrqPJrfH5Xe7/FyX18rH9XZ7lgJYtFOOMcc8IjXYu7xb1xdmLI+g6Tnx0vR3npxCUemqBzJSX3U2duYQllAgjF4dYkuvCXL8nmuLaYR2iFPbysSq53cS2tngAfAwUPE8kodduwBmkpciGiAsaA3Op4HT7V4gZT66LX7GQmmAXArM4GF+NuAm0Q/5c9jDYIQyYAmphT33enkuSsUTtVaexFh9D6Ckn6y4rZjyx0qkNYDvyiqn7d0Ptqr6epdDO1HAS+kCMIAzYTOHw2iHZQt5w1OwJeiy27jgfd7xeO0V2PNtoGyFaZlzG/0bHtF3DU6LpwsfTuBrPnd9C8Tct36+5XspBD5bGZhOf67r3Yc2XPy7n7dRejnwvfl24oF5avlfsuN8GB1d/mpz3rFR6aoHohAe+momHJLskidZWwOoHjAWThrBn9fbt4B5LX2vbLGQ2Mok3g3Ic3yFmZVnWVPPBKUFC1qgkWvQ1VreoGbl5sHs51vIoGtdW4JCvnbZLXzmuUMW0GTBNhjZ2iavl9WRn8GRT0J3/IXfeAHMz/hwQfoq9+gb09Zubs+V+NRUUlve5ulLL736eGKn5nv3B/eYB3HZPkaCOlqOkYN874P82ntdz9rIoiyOb8H7yvMYQ25DhyNOyQq++eYx3jwneVK8aWjwZ5w4XysJ88xy28khkqWsumw/53a63gZeJyuUxePmnw0J+rnrbOVgBbGUq681j9c1/9kQcTIPKSfv1njDG95Altc/vv76pzwEr7l25d0xrtiCYKG2d+AyuM5yUO89QJio3/FXAMbhHxQCRzszsFEIXnXjQXdqS8dm+69DBGhmt8+dsCwF8+wEUOv55qkHskNBfr7phqeU3WEQzxuYbwh7eG0lzlwSiXAVoSaeBxB7iaf54cUEa903ZTAAu+9QJGtAWo7NP4MJx4T3AXmWScuwvdllIJkG122ZNB/smSHneCxW+C4XPnHtxNu+3gYMdLUM+zmXtcAXMHW5xgjGX8+3dD0N/KQ2BrhmkF18+GRTKxfq8b2mnbG0FMwa3ydl2DJ9MjLdh21Uue6mv8NdlV53fKeyhdDeQrvRrswdZgJ8bk13Vlvc1IHHYDfJLjOWk999wGonnndII2VgyeLm+9A18thi8nWvbwcMALvlctsKcqc2gK0BYHBcyTxZgkI73Cd8OlzjcFLTB2jlWrxGYtm5xouHeME7PGNlEBPQ8IO+8lHmPQCgFV73oOvvbXggS5Yj0ksvPR1+693d0EdyKItk2hukLTcG6/Z6qc8bzqDZiyFy3/NqL720Q4JWPq6/FcLyNBv4PLY7nYCmr2FUJNmrulVWl+f+6slyAymK2uNh0dWge18yrj26obROgNtpjfmlYLqO0xinnh4Hzu+ohWlzfUSDnF6lEDj50w1sok4WcHeWB6hXqvhcIT9rS40E8OaavQiONeDF5IQt7AVQfkDLG3xywmnvYrUF6IHeYOQQR2tn53O7uo10zEr3DTo/10DfZXh+xflXe9wWg4rnXDwhy3JhK24WF0RZ8IwVkC375ofbbIV2i7cAdltSgHiH/2hPgyqyw2oeGyme4Pb8C/R7vuVEi/9sPDhM6gl5L7dN3t73YB74mhVS521gWGPNluMJXDu1YofH8MJK0CB1X+rIQuOK+9R0uF1trJ6At9NStE6tBJZSuPUMvynffWbMsWJ1GQszT7R2fj4pt04+vaZ+Qc5TFa/CbfU4Hm3rcgmiB8wKs+ABEGJw3N8Wvw+vQzAcxkCA8ryVBgywGw5T8RZsQbvdBgf/XiEcP9fltFXTQuoOPlk0BtWepPWAaCXUYGwXk/oclnIZWPkoB0J9uU4YCXpohwHaYHifoJuPpscDwIqce2u9t8s7KWJCM8gKvEpZPscKr9Ag33Mf9LEVmj1syxG7vWmfT2rlnvuyl1PDn9U+A4Z57zBY88O87j5ZSqhBn2d6jpHrjK9+hrQ8FMuvjYIGUifur/0ubUkvIO0x67ymv9vdRiQ8NT7a0HIZ5rEVGvd7TCwlYP65fhKRGvKu9FTIaFltMOIkIAY1Pw/hhBAQwqWtPajy3Udd27PwZigfGsZ3ysL9z/VsaMsna+xtKS4ty8AwgHmgm/m0f/GuO4o8DcQtSB4E5GmrqXnfz+CZAdCAsoHM/WRgQ3HQdiuFACaKGxBFIdiSs3IBCAkxtWXqQWpgb6PCCpsybaTQr9yDbh9fgrUP/R3W8M51y4N53W1sAGlAc+gMpeJ+gl4fveL5kh5rt8ISPbb6WfJaBlr+FlCuMX/CCq73ipv2zprONaaW1+M6XFanVjz97Cm5bQ3U/luKi3yrrJNC6/a2MjgpLiuO1f5WDlzzZ6cHXQBC1YxoILNHYKXRlTYzAKp89xp7jqewJs8gYbUFeXpikbKxnCzs+U0M1scjkO/jOsuHZ2wlNSM93wHIeKnXAvfVeScB7Q5uRdN0rmfbq0D4TDepB4wtR4dBCKukL3IkRZQChwf6rP/wwpsU81xCSOY9PLds9YqUjs0D6pThtuFt2SrkukOWVnZ4lxgqTIA7BOalpPAjz+Ml2UqHdw0W7rf2pj0vwG+UQi97pBwrwAZ3l31aKQT/LBPPmgwurrvlrcOQppXvpzFy8oiTegK/DdQl95aP9gxupabxpKQ8fm4pmu6rpMYe86Blx211mR63TfsyVJaR0+nBqQEmxMv9Vt62mtygboxjjAaCKIMATRKDOUxjd7FfkWlLyO54a2DKyG+fUe8lYgZhC44HpflhIb8lBObPElan7nxbuN2xBmkLlJW2k8MXXkEEOHa74CNnTJHYhwB45ll209JHuWevLfUxp5By83w+8RROys/gluR9DuSzHFBO8ttogI9e4ZR5JJbGhm6vnOJIEJ+siiy0sQENDRod9mjll/sODVGOVwu53ywzy/JscD0pjHWvFdpJnk8A6jHS5Voh3AJMKzSHmldqOWlFe6J7gSllrLpaGdgw6eeWUdft82fzyCva3Kc800Zk484t5X7iy630YDHDg251KoPdFk4zoRWBG0LnEyvNvViWhCP8rmPeIIVi6oY6dOAwBV5B79pNWha2gdZAhNB1Rzgk4U7ua+bJyRpyaMoWaAO/PYB2LambcEinHkANUn6mrTyey6d3Knv1mENT8Bqvgo1qPlvKwm55oy7aQVmANfkJEbWy9mF6yxiBv+5bT9xariwzKM8kwpm0ty2vE/CZLjwrwphc5/gQFJH33DSYIPO2hm0JLlm04bDkcVmVp3Y0QJ/a3PUhV8u78AS67/fE+ql9K4/TUqydTFsDsflt48V9Yhr4bBnpuvu5pcxO7eklyR677T2brpWeUggGndZq7gA6uYldAtiAaWL9wvtc73fwXi6v3hhiV76t0Z6E9nO27lZ7qIfrVho+O8YKzTFRd3h3bA9S+Nz8MS0GzWXh0EbAhfIM7Cuc1Qof4AkIsZ/Dx1KQH+/C+wooB68BGgBu+oJ+AJAdwnHbbZU3P1rxNM/ao1syibzR5q4ffltZYL27P2xNd5/bk+lBbRD3nEF4D13UR5/SN1bM0GtZWIaZ6+b7c4fQivvrFpi67l5lZcVEOwByG1Cut/vbnpLpgO+NIaZxyXqS2+xnuddYsxY9uG/Mc+exx2c8aLlcBnSvbHQ53c7uQ7fLdRobu49O6eY+hPVnotwBi4FmRicDCJagLTKSwx1WVDSYsnpS1BafhdTX3Na2xm+1i/z87rr6+7IIWugb1KjbbTSgUJ6F2wrLg9bPmm9Y7lY6fgcFtPA2OlvtWNZJXjYJbdTrc3hO1pTvOdzTK5Y8oFpZNC/h091Q0u3dIT+U1a++bAXa5TfIuS5/d3zf4UxPtvOXa3mfB6uePEfhZHBastcKYX23vC9AWs/42eZBl2nwXUrgcjmHTJqHblfX2eNt9b1lsfP0tTX+3UeWw1ZGJ164XT1+7+62N2OeOb/LMn/JYzo6mnBKR4Xg1B3eg9vEmzBfb41q94V17ViebrABxsANYPW6ZwacQy72Flq5QEMruFsg/WhYBN0BS+h7cHRqAUhq8PI9P7OWdcJbLxO2O2zQNf2E2uCtwxVYUPRBfrOs17QZUN2OJeS2vuFlW3DQ57DSCdQ9wOzNuW8p254CdcUi9/xHLzlu+lsh+M98twzg4THJjSdA+1hkkYQn0YqwZYDyG8RbXkz3LQVGfj+76mtZ93d42mUt8O0lvU13hzs7fOO8HQkwza6ffl11Nn8tK7fGuo3Pvt886jytWBb2WF6Xh9Pebz/DtZXmslMLshvQluZK63o31t6BBz6ucgsbyc/5N8LifQxrkHTc8qTQklBMWHEGG7tip8G1eHLqdNdpWizoHQ88CZXvLwWwBMt1oGjz51BUg5rb5X0j5mcDqcHfv33N4cOWt1tydwp1LFm2kWEDwuHIxaPmF6uU2pNofiEzqTfPZAMo3lYmuel3FBHl5Xfm0jickXSS2yUP5sFpbLaiWUqln3Fe959DSB7vi8amjecbCD1e+FzX3e7lga9QlPvIMrmSjSbX1977qY0eZ823hU/mt3lknrVn6PZ2JKHLW+m4D6GZT8Kq8nK/rmx1vhlNmR6M3RndMFvhHmCU0+vK6ewVJvIA707it1c6JdnadTm2TB2zX4NvDcwWZtMEv1sAbL17EtnhIXjEpqtesmswa0ve4OiYKfVQdhLhjm6Ll9W5P1f76QvKZ27EfbYGnfkFfX6bVYOlvRSSJ8DZ54LXRBt4SQ11UBaHJmYOxWcodX/7d5RBjpjnPSAf/ehHr58JD9Ev8UxyymmuM6cGD2wRNnCu+lqenHoMnJ47ySv12mvzdSuEtmKbDn+6/vYk2jizQuaaPT/T0tjSfOj2tZy0l9p5lqHWPD6Bd2NT89j8bUxro8eGahs1jpysdPQQmkhrfUDIDGhmNBNaYD3QrRwMqn62td1zmn/wBF0Pig4/dfzNTPekdFvXfsbCQX1+1u1bioHrp1ggtDbw+VkmWFsxu7MtKICK20dbfFy1B5KVQK4x+dm7yTtU1/xFecAz93H3q/mJBW/QsSxQJtc8YG1xwUOUPM/hkXpPAnVbqXqSkTo84d7GiS03wm/IZxRC/rLkNZZ/Nk3GS8j+jigClvZ+7ud+7hMFlZBcz4vBszU2fN/34FEDgkHKoL7kzv1AnlZQHQFwnzTwtRws+hog+3t7rj6apoG48cNjf3nAy5rPdy+w6DKdLNetdHzNMmu6vI/C8mz6uO49Rq0Mm3cnXHpwqwGXy9PAZo23BGYR4EYnuRELOC1sji+3JflIlmu71AYIM7xpJrWrZ6/F9JhHS+svwW+Abt503a5zdWYrD/MBwWgr4dRHPOewjo++pp8MoPSBAZtYOOURbkoenz5rL6XbaL7CSyZ4PSAsf9DVfLPV6FVNfv0qvHJf2QOj3zgWm2vImo+6RkEYjNhHk3eU//Vf//UV7PM7byXMn184xJ6IfEdRcIBgFELKaMu65cI8bBk7gdKS4VMdJ/lFmS7MaHlb4737jvvru3FnGR+rLaajFVKXfeKxDaW1AMbAvZ5fitXgT997zLWCcNvcLr53uM5G+pKTlaaHgLb0e3Z9RHJbwTznT7uRra0N8EykxTrymfoZFOTxxiRbYIBPd2ILpGPzpsuDfAlPkpVKxzcNEFxbXkh7T0thLd7RXvI7n+kzgHqwOPRC3qX47M5Thi3jBl2Hq7hnIM7znosAQH3WlI8Wodye/KU/7CpjtROm4ZWrvJubZzwQAP3QhkJgAONd0j5k3mcz2fqnzZSN/CZxxhPl5x4hoo997GNXUM/veATs9AZceNcB5ScvPHK5twa5V6RZtniu5foEOrc8V1IDfcutx4jLN20eS/AW8DzJfT/n9pkvDfS9WIXkMEuXbZzqc4C834V+O/FqJXtVS2GabvPOxqXHor10K4VWgF1Hp7kPAQJ42Na5AaTd9CagrRILjAGI38nXZ9F3Jxvw/Gcm8nxrU+63ojKNfs4CYcE1TYs+t8d1mpa1EaxDE3xCZ/OvB1/noZ62bFuwAHVbtg2qxNHdLntlD+o9BAgo9xwCwupvPhu4HOKyh0K9vHe4PZu2kKDDh/Kx14K6MHrYoIYy7DBQ8zbXA+55nrf78Ya+/LExL94B9//yL//yydwDHgbKhz7nzXW0k3HRQOi+8DhYHif93zJjA8byy2+n9bvHe1vI1GO6odHPInfI5nqmwyk2mrqtxgEbPJ2Ma6Z/gbXbZtk1/5r3prd50qmVwRrjtMcKoelpRdr92nU5TQ/BTO1BbqEjtcewBOXUUM4v8uoS6loMs3Vh5rRA2mo/CeWJ1pPH4XabT1YWrVC6zh4EPXDXAFgd2nmd7AV1WRwdbhf1pLAAygCUy/Hkbzw7h50ChD4eg/IBNIO0B38D7YqZAuY+P8mKoPvGNBMeAmDbwKHvPJ9iWTINXAfs0+Z84g2gDJIC/rkOP3klLNY/MovHgPcUOqJsUvfnfM7nXOtNHQtsDO4tz+SxnJon3F/jzWXwfclyG2Huy1N5NqJsTJ28E5fd5bdx0Z8o+DVeqNvy2HjV4Z2+f+Lhqf1ue7eTZCXW7YRPjdOrbxd23EpPHW7Xa7lNCBV5UtXC1mWZCeT34PXEJM/gIdhahnmeoGuFcJ9WJnkCsP9aqG4NhrZozXh3VC+BXcJrK8+dvjrbvLEH5dTuqOt3eMS02SW21YF1HVp8+CAuPofXMYdAGexeJq8Bj7Y5DGkr0X3kMFGSw4c813V7ot39wQY76DXfHfpy6MqeFVY7yjTWfuYCAtT5jILIu2oJd6IoeY0r/YsyQNEwBlCweDJ5Lr+TP+/xoE96kNuDasBw/pY3y69ldcncUgY9Zlq5uy7LI6lBbI2HLp/r/XyX6/KsDHoJtGXGfHMd8NjtJo/r6eiK6Wl6m2c95t3OpmMpkpYJH7Wyoj4LG5OOh9t1R1JIL1Gkwu7Mfq7B1IS5Prvq1swGr1YEq3Guw598RykBFCnDew5cN4lBQ30+sXIBPr87fOWBxG/ochjMg2BZLeYvbbJQLwBAmRvYewCh9G09NaDgPfRqC/rKp4Z2Xd1O/266HQ6Adh+tYa+MdpyAKb89d9GhBgA8nz4DywrJij4KAaUQwObwvtzLd96/4WXRNn5oU5JDZcwneJ9EEp5ae6Lrc30/eRA9NixPvr6Sgf6WYdgKpNOqq3GIPvL4XUZRg2MD8mr3oq3Bu/GmFeJJEd5qa48pZM789PX2Xo1Tp5AWvxtrV3pKIbT2bbfZmrFB5MRQl+P8lOedxAZoW5hJnkCGUS5z1U1e6uRzhSrcbr630KyOt1C20K20yjENJ4Fu4Gy6u9M7vuhkMOR388CKz5Z7EqGnJCtxLHXLT19DhvxSe4OK24NMuGy8DfofEHYoyUvwSK1o8Tbtubg8vyUuoR72CCQfS0gBcPgNnwgn+ZReKySe40gQ8zWJY989Wem6/e4E03xfsny2kuf+fZ+Wszb0eky2fN4aFws/WuGs36fnu6z1PamVysKTdX2VeXq205rExgBo2u5T4Ma31befDF3TQ2jmLKJogK8bBAzYTfiyFniOZXa8kpFy7CHYymq3aoGpaWsLra13x9/5I9nddPnW6M2jU1hrKZZVpuky3zzwTLMBtZWTBcYb05I8meqVP14mihvqARnQ4iU5fsZr+U1fr6UnOV9P4uK5QR+n4pIHi960rTX77s/QG0DnfRn2SpNSXkAZeUo8n53D8IgwEOXlOiEjL5elL5lIht9JhN3wtLIhjRVT8T5YeUdb2AjokMACQvf9Mpwa0E/GwDJMeuxyrT1q0rMonv5+KsffOzTabVrl9b02gHucLpq7TX7efFhl+Xn/UYbnMfqe+7IxtPG1+7PrPKU5qWxgg4gm4PnaxGVwoRzKcCPJT4zUSxYZVJlEy+DLgOwB3czz9QXIZkpbGM2o9Uz/7o68u9shq7YAelCcBKWVgOdL7DJSpuPwBsk1iQatzsd195Pbnr9eDghtSQCWQx8P9OIcQJPfWPLURbLS4tN5eOk8h731hh2/28HWPp8oPJ5DsUTOUi7zC7SPsA+KBi8pSoTfyDFt5BNlALizqol2MF8TvkXWsymNuQrCYVESkX8rU4yhtozbO18eZAPELfDoZxbQ+94adw3eq65OHl+mx+33s02r6T/J1il/f2/Q7LYtxbOUgttGoi0nYF6Y4fLdBisPKwvjtHnhtqw0PQRbHybEFpxd7xWPdjnO69UtWJUGKNzyDBIf6mWLr4+2bkY32DdA+pPrPnLbcWO7kwi/rXXiw60YWDZJ57CyxztiDY5YqbZ6fOIondpewMf1Ypeee/E+Emj1HIgHmt9nQDleAuz6CYNwVAR9Tfhj7VmgDL+ABsvfS1aZd0jeADUhJ3iaeyzvDOBSJ7zIZ4CWduY7K3tCA+cIpczIWRRC4v9Y4T4LK/k5a4j249Yjg+xDyCergrDwP/MzP/PJqiXaYEMm9UN/8jFBH3o/7/M+70pTysjveB4O67Fslj5G1noMtGfJdxtv9gANMu3ZNlB7LC0FQDLQtqFkWTZOGPRaGTQ20bdNq73O9lrNi7WklzJa+XTUwyDt5dqmbSkY+H4KAfOcPcluuw20xuIOifteK3ynmyGj1khm/Cp0aXgnM5B17b2yIZ2WwZHBzvZ+Nx6gtuA0Dd2W1T7KdGcY+D34fK+tM5hNeZ1Mn5e/WQD952esCCzgXU9bAJ0cbrMXkOSQFnV4roA+aYs+ib5KHuLtKNPmabeNd0xwzeWTH+/IRgGWON6l50lQtoQeUaoesISIkhdvA8s/1jp7B7D8aRsTxlZwnVJvADwpAzjnE1khWPF5Qj4p4B+ZR5Gw3DTXEzrKtdDIkePU1zLpvrTsuU86GbDX+L3vudPvU3J9fb2/t2w6TE1qRdDytBTNs9DUAHyild8Lj6CpyzS29jg3trYh2/3jMtoAWN6DaV9tOx5/vZhzcnW6M8wgl+W/DEaWMWYA5s/ufKwlhyLcOUsoTEPXuUC86W2Gr/IdcnIeGG+Ac/nWzgCErR4PVu65TsDQcxiPhiXSgr/i9UsRoRS6v2xNmZdtweEN0Z+m3X1n/nG9BdUejD1De50A/YN6gxq04IkkQRtv4MPrZN9LaA7YMkeAgkAZsWGN14SaVjw+FG5AP2Uhv/FyCYN6/oCjPVIG8zUoDL+WlLmZlJ/ystv5JLMtL+6fTgu4efZkCD0L0C+Q53qnZcR0Pf3d8thl9BgwXrQMrfHtMu6j6wSot9pumnocrGfhpT13j91+xt/thd3Kt9LRQ3BjSAibNQ8A7qVwjv8ujcafT5XsZX4ZGCgFBp2Z2UvvzMRTnaudfd9Ct4QToD0JFOD6Kia/YgneGswnfi/3uecHuk8ayO0V+NNWVCs5l4GF6/wAKiEk9h1YWE/K2NcJEZkf+fOSXurrvQMBakAc8AyQekUQebHwkwghAcAJz3hVUZ5DplEK7F+gzSgOdjeH9tDw+te//snLbfJHqBCPAHoo30YEBoO9LbzlliHLYRsgrQzacmx5a+uyjYAT0POsyzopDsuFx2mXcRqnBv+lzPz8Gu+LLyc6b43JBcZN94lnt/pl8aQjD7eUUt9D1m2U3epH0jy64qQQWkicP3+4s26oCfTzWFzMGzTw5DdeRIdZ3NgG2UXr6iy+O5bqAbXa3t9PneHOhRfMEQB2dmcXcLv8O1nrJ5pdv8NqjqOusIzjmPDVHoN5aMFN8tlElOuVSMwVBCgBMIekWtbgBbF59xtWuhUIk77E0gHtlJG8PI+MocSYCGfiOMAdyz4WOO/2tvfhd33byqQ9KR+FhFeQ8lKueWAlmDxpI0tWzYMk5mZQEowDKwP3WY8Np1sg0ADU8fUuo8dBGxT3AU7T1UB4Aj/LYM+DJfVCiR4Xt3DilEzLKSz76KBk2tjs0KyNukUbsuLogOtpI7b53wql5x5PaZ526u8trL5v12fNLXQMz/cJFTG5CJDgXmegMtGWfHHpl/AALG2Z2wJeDHBZ0N5WG3RSjjvRMXAng7Y7MinAAiB5Z629rKVQvPTTiq752pZe08yg8eY0891WchJhDhSa+8fzBdRP23zEhBV6x7e9HNPHScNb6slzAfjIgIExzxIiSn4UBgqI+ScmDb1ii+dYGRQ5y74BPA7KxEjB+3Cfe4I8iqAniFEC0MRkOTzOHAEb2Og3lFWUCSeckj904JFYdpYXucakAZX7nvNhrFgZLmCxEWPZMx2uj/s99+c8ro/rLmeVyXiz7C+6TsrMn22Nm65Fh+vn3t3d00t5W5nA725v8zqpl197kYtp5nlkwXOVpzD/SRFOhbCsNhrfzF+HtLXGasBG6Bwj9rEICGoGWAZGBkwm1BAonzTYTGpgXJ3YbaSjoHMJkAXVQka+Hnh+DjCN1WirduXv+O8aIE1TW9pNd9PaFlZ7JT4+24DDsRf0m1/P6U1cbLhCiCnXCwga0JgQ5nk8AZZhBowBxyRkgJAVdPkIDgA45TFpi2GBQrA8JU9kDcWCIswznmPIM+yXoR7CQ3kGi978y3dWBtFe5jp4WQ68Z37Bbem+7X41T5fMr7w2HMjbgOTrLqPBytjQ9VFGX1s0N5j7WdP7aBhGXdfiQ6dWML52i49Nr581DY2FrYgaO9yW/mwFQJk2hM2f1feue6XjG9PaReoGNbPaW3DjmkAzoSeBqAerKoM4n1hHfgY6TsLXbWj6/b3zrvY37fxuy9ftbeCztd98NM/MQ0+su809AFvYnHe103znz4ffedOaFSe/WWOPhwfNfkk9Gwyt7NsTQmkCwOw3oHwv14U3yELKSl6HGr3aCPDlkL4GxjwfzyAWfUCdzWq0l+WkVgZ4Ab3oAYC3MrVxQj4OBPRYccLIWh4uZbdyvWW8tEzY4mxgtVIw8DbIrnJbBlu2Sa14GjBvybFlv3nnNnRI5kSH6zyFhVa7zLNTotwO+VgejJ1JbTiTLAvGhhNO9/P38YI05xAsCI7fw2xbB10BAwZr0SEYE2rPwp3rcEXKicXGem3Wn7sux667Dc7H51JS3T4z9u7udnzTii2pBcTn12S9OzyFnlZoflmNhcXCbqVhoO5Ba3DyPgvyN+hDP98JT1BWQNerb6jfxy2Yn6Gds/8pCzqi6OMx+cx/ZIv8ACcbuHI/PESh8MfKISaRvb8B8O4TUlMGewiYT0gKTXk2nlzqCv0oOwM9SilKxMty8xc+5BkMmVzj6OvQ8rrXve6Jt8BeFLenBzkeB32wjAbLYn/vcWD5pu9PyqNlz2OGvDbqekyclIjnuJbhdDeMPO8obzoXULcSablZYNqA2h6Q67U8nZRfgzpytnaaGwMd/mls6n7kmfXbeW8pL9LxcDsY2PHCJroBkXxr+Zrj6VxzQ5ZAE8f2C1EMmItJbsPd3TnU47yn2KjX5PPbG+O67V0feVgl0orICgfAWX+mtQf6GnzN50UT7UnyBCnhF8IYWOCERQDvWNMewF5tw28UgIE+9/xuAssaXgIgT9sCiD7tFhlM8rwNStB0IS8ACi9gSkJhwMf8jrKCdnsGSyGkHHYi59koEDa/0d9RVFEI8UR4+xnzElb4yHuUEjyCXuYj3C76zyuwfM886tVMSSirNhYMipYx3+ux7bJdd+c9yW8rGdri37b+ewyZxjVmehz4HjSe7ncbnU6KoNvc9a2xbSW/eNp5kzzuT7xtelc+0jzczoOLuDGWWJJ3GC8mumH8biZ32MRa2G4TS/4yqOy5uO4OQQBqHrzNxCQPRkDetJoO3D6Aosu61cl2wamDwWzLPgmlaS9izb2Yh3wHeLFm7dF1H9m6JPZu8ABU6a/k8YoiwD0JBQB4AnT2LgBQr/bhHc3djtMksJcnuy32dAgxrUHCfIOXgHqHsCc9A+yx6PEi8FTyybJUe0vscbBSzSmoyRsFk2sogeThSGz3Sz5ZdkobaA8rlqJY7PGwYgneehw5taUKr60s6Wt/8n0Zbg0sa25qKZCmq9fNW+77uZaVu4MBZBlpb8HtWs/6s3nyrIqjDa/Fny4jCV40zQ38xmmX0x6W6bqvL5LmpDKVMXiSfDyDNfZK3cnNUPK4zs5rIGTQssLEz7cQIYhYns7TCqjb04wCLG0BAxy2QtcgscvWllLT3Pxomm1xdZ41IOgbC2ALBu1r0PSppv4zMNv7yyfAxgZD6GqrFuUEX1n2aZrSz/ymXIeTkC2DDXKa6wFfwio8bw+AdydwoBzKzF5HXnLzR3/0R1dAzzOAOHlYCYSC4tA75jM+93M/94lCDA2Ej0Jb9jwwP4YHQFtseOWoChR16s9zX/ZlX3b5vd/7vSfHhCT0hCcHXy0X7luUmpUA/cD1pPbsOxRhYPEYXXUagE6A1PJ767fHyhr/axzcpwh6XJ7GUz/f42aV28k44LHnNt1qT6dVB8keH79XWzrNSWUexhVOIZwrQ+GOn7nBlNEavhnXnWqCKYPrXnHEi8oRcDewQdgxOIM0eZqurr/5YiH3PIU9m37Wz/h3ewarHuc339Yk8xJG00S6pcTd93gj9gY5p5+ds0woG+QBtE9o9y7gjofR3gf89JJW6nO8HO8B3nONupnj8J4Pe5qEozAYKA/LOtcSzvnjP/7jy0c+8pEn4S1vmkz+yGA8iNxL/shjwjwsPc09H7uBl5trHGfBnBh9kva++c1vfnLEdVKU0oc//OFr2VEmGEUpPymb4JgA92F7tCWpw0HwzEYO/UC/2wPte4/K8m75IbVx10qhZa/ltsfhfYrEdDZdHqeWuU4n0D+lW8D6aBiqvn5SACf88f3GA7e3cdZe74n/TjNkhHXVndFLM28BURO+8qxP8lgoUQjs2Fxan0YzuPjjt+teoNvtMfBakVjpUIYV3+r85ofzNM/Wb+jwvEUrty4bsGs6ekGAB5EB20oHgwBrmeMZAHvoYldtEhu8OFE0Fm/Ak0lzL1ZInYRBQl/yWdCpg/CJ5zkoA68tz+Y3ngKKN4lYvOca+EOZfOhDH7oCccohFJS2QF+Sjw5PeW9605uenEOU5zJXwGQ0tMaaJ9yTfJkkD9iHbsrOtfAr9eV7vIIoptCU6ynrDW94wxNlnY10nIiKZ0V7OHyQ0JRDnsgx5z2RpwHcfy33S85XeNb3Oy0l0s/6d9O1xuzCmpalW6DrMhY2kRw5cLpPEfT1W0p0pVbWXinW5S/wv6/841lGjtlaCSziTxUvkPN9f3dHOBaO8BOLxQJr4TOTHS65RZvrXsLZmtU0vzQmlTstBUlnUjb5Wmm4DOdfnpnr8u9Ft2k2z70ajHXxCD2T+n4pPBsHAUnqBARzPYqD/QjpOw6XY56BfqJdHCjXiglrm3g+16EPK5x8tAOQpn25F3oATMpK21J2wPy9733vNVSE7BMKclgln7H0Y6FbESTUxEtxOIsIsMb6/+hHP3rNHwWZ/ISwkjehptSTskPnG9/4xusE9e/8zu9caYpS+YIv+IKrsgjd7373u58owCRWY3HcS57lHgsE1rhrYGsZtAFoWbMXdpJfp2UQPUtaY9jldbmdv2lqUGwwPeGU799Scq0wVznO7zzGwqYryXN/8P9yaFNHSXzvk1IIyczg92CAmLYuP5nUINrxdZLDAhCPhcfgt0CeOoL2uG7T4vtul8tr5baUwFIsVnCu29b9iuMvQXC5VpbmXc9ndDzYdfZ8BPTiKdAeh1q8bA8LmglTTyAHzFgmyqqgKIAAnpeyUiZt8rEXHF6Hgsh1NsNZZrB6c51PDAgOhvPkL0qMMlAOTCLHIuedyEy0Ym3bs4il/kVf9EXXpakJMQXcowQI+TisGl688MILV4AOiLN8NDz0yqwoiZRL37J8NUog3kSUDe8KybUv+ZIvuT4fGrD48aJZ7YRCd/+h1M1XZMzGFDLSS5rX2Pf4WHK1rjXILQD1WOpxdRo3PQYXqPcYW0adxxZ5mg/rmRVqW/Wf8HMptIVRvVy460L+Vvmn9JRCwG1v0HAjzBhrKAtWC093jK0SCyH3KDPfmczLYItV9id/8ievAjwvZ0WReAOVGbEEmzIcWzVDPZHMXwsi+bz237wxTzzwzIvV6Y5/Imy03aGkpQyW0oKWjjuuvrUCagsTa9tWJmCL9Q7NycdZP8xJEOpIYs1/PjmEDm8BpWIl0uEmrz5igpfjIJg8zjMpL9ex3L2UOXXGeufUXa+6gVcpK39RBF/xFV9xpfkDH/jA5c///M+fTGh//ud//rWegHFSvkdhQH/ypu7IMZPKLIFFeSJLUVDJz4T0+9///mt+2pi6QgvzBfZWUn/a4lBJ7/3B4PJeGRSI8/V4bZlhDDh/f19jzs8tQ26lU54GxaVcPIZMi8eV+7vHK9d6cni1r5Ov3Re96LHY7VqYlmS8XtecTlGNOanszUqA5OpIrAsztsuCqHZxDDYGPhNLR3iSm9CFAc9zB62dlzIy07hucHb77KkwqFohEm7xbkTKWjyBTpZXUl5PFrusk6XkAeXUMc4W3iVwPNcWlvuAe8TikRfO3yFERF+nrwJ+7GXwURSeh4j1GysbIIu1nBh5L4f0jmh47HsoEFY74Sn0m8sCqCgKv+zG/en+T/grfwHgWOah813vetcTq91HVziklfJircfzgI6Uw7xD7v/hH/7hq0Jt1Msb16IUQhvnerEyCr6Ht+EVdEch5I8l47QzyTudLTN4ee15GjAtBx4TJ5m1rJ3k1OP0BF7ruVVXj42+f3quQ7DPonSMHY19JyXp34yrpXC6HNKtEHM/18qPdrr+xZubh9tRINZ2E21QbBDptJjj51rjrgYj3Bl8HrRWLqS2gBsMm6lt7eTPg5PkQWM6zXyvonEeW/v57d2zrVTdph4op5ju6o9ulxXBSSgoy7FHCxaAx3LgrK7hzy+VD1AF/BL75sC5/OYgOUJJ9GdALcAZS50lqdAI2KN4TRMDipfb4JXgaXB0BRa5PTkUcT69wxgehzbKz/eEfkJXrPV4CEz+suua13EmPJa4f57zmfbJE8v+LW95yzU8hDyFH+Efb1nj/KWUzaqlzEOwHJUlrezODi1slPOkt8Nja4x6gr5lFdn0PFGPdZfV8tP5nDz+XMbJqLyVFi2Lpo54nJTJMq58ra3r+5SJeUGdjU88u3DF+RoTF55Zwdzqh3XtuA8hySBgK4EKIcwrOdpqWNqsAZq6GuBXaIrJygyg3PeKleW+9UCgfjPYjCPZq6HMVpbQ15M7AJfzegDaQlhWTc8rtELxd9NqfrqtDfy3FMHiF1ayrWws0wAfE50BTK77PQD5TmybTzaDoThYW58U3gUEYxkTOqLvAX7kkbYgB7yjgPzuW7fDbzPDC+E6myDzTK5FQeXza7/2a69t+d3f/d1r2JKlpljy7DLO5xd+4Rc+CdmEDt6bHP5EIYRf+cu9KIcoD+ZjUi/KK/xl4ph+8Q5vlshmPCQPG+QI59E2ewv2yq1M/dvySZ+gZEhtWNwCf/rBn9xr8Do9v34bn04y3c8sQ9BhoMYg09Zjp8NPnZbSMWac2rZw6T4+O53mCF3HSkcP4aRZ3PgGoQVGboitYfIzUP1MdziDNvcyEIhJE/s1ExosDQZJVmgrj4WlrSaD5VrKCm+WO+6E8vNqGl+3Qmi67u6ePp3Un36JTG9Ma8EybS2I3u3LDltWeQH4Ac5Y/1znpTBWHFjiPrkUKx/rFTDN/Vi4AbcAawCTSWh4k+RTVhtAAXHaSPiIZctJpgU+heex2L/927/9mo/lnqym+rqv+7rrxrB3vvOdr9o0xtEVLHTge/gQix4DhrmM1MkeDuhI+CgKI3RwvAWrsPKHR8UrNHl9aJRSFELmGpKPyWMDj8O+SfCGdrccIGs2VhzuXWDM9wac+wwoj3+PuVPo6BYYus1OzwKePH/63mXfutZh2pU6rO1rbsstOk95HH42Hc8aFpuH260JBzrPVgGVteZaDT5pUN9vy6XrJ8YKAGWgs0uzgdF03ap7WQVJ9jrui73fPYMF03xYoEz+W1aSFXLzGR4tz8r5SA4lWGEmeVkmoaHwPX+sl6cfAHe/q4D1+0sWAFO8Cb9AJylzDgnNsOySFUABaO9RcL95w5rnNvIbLyPPAPCArvs48wP5S53Zi0CoJmGbKISAciaRcx8AtmHiJbJJeS48ipXPZHpSvseDYG8CnhaTxSkDTwqFkLJ5oQ7tQxngCaBwUK6EzAzo9FGupX3cIyyMR8F15rl6DoF6+H3ChSX3C19c/poHPKWlhBZgO49/dxy/w7GdnzoMsKcx5PnHRasNTNN+qtdpzRFQhpV418X3E09vvkKTChZD+O2VLs3MbmCDNs/ZYnFqq5wVEBxVzOafZWl4snsJoNOySLCsmpEuawlFC5MVjvnhvD1wms/u3GUZPItVtITRdPAdsMGyZyIUhcDKHN4IBvAksZM5IMekLqBMv7HaKM97GXGeZUKZUEsAM6CKJZ04ecI36ffUkd9YzYScKC/PszkuoJj5iV58gMeQFJqSL6GbgGzK5gU1b33rW695f/3Xf/2qFPKsX93JslTzHyVp/nPSKfMqeL7Jj3KFJnjHcRehJ39JeGzMX+S7D9SLUvGej94MuOTBhg5A1nNnLZ88f5KtLr/lfYWmub6MnRNInxTHGivreXtFHSbuetv4Oo3F5kGvKFw8W8Zr4+mtdqzQ261nVpohoyUsTSCC493LfutVd5K1NoxhcLYg9bMGQlusPrXSAuewyQlsfY1ka4fUA8NWQCsS0+k2AUBWToAF+WxhdZzW4SkP2ObPiv0uBeQVMCg1r3DCMgR4sCppLxP6rovD7rBikwj7EGZCCRBewvqEF+zSTZ/G+uXo7IBe4uyJvaff2VlLXbxNLfQl1MP6/oBkvjMIoxRSVurmJT65FxqZT8j1KAXCPwHkeCtREvEO0l5CYlj9PpbavHMfE3JLaIgjs/FAOEEWRdyyxLlFbH5js9zXfM3XXFc85fn3ve99V6+GCWUOyGO5rYEI7w1Z8zh06MhyiQfklXEtt8swaUOoPYUe8yjXNVZvgVwrns7X9TUQ95ihnJ4j8HXy34rX8935zQ9kZs3HtNKxd9bWv/uk6aHMUyjOaS47bQVg7WlizHga0Z1tQruD3QmtObs8riWx3M7ur/N3zN2Dq72F+4SrFZnbsBjc/GsBbQvE8VSHG0xb87fp77kTr15qN98KzvdaWTMHYC/BfR9aYy0nscmJiUusWo5T8HwCdACGAbh8DyAFiJMC5NmIFRBOnngFiZcnrJKUGH6eSZmsYOJYiIA+nkZAj2WtqTM0ZZ4g4ZrQjoeSMhKeSh7OykrYKuXkmcT4f+3Xfu3J8RL0oSduCR95/gRjgHAafMODSvtyLfWz0dKWMYqF/rBXk++h8Yu/+IuvZUQRAuy9NBeeYyyhDBaIAk4GoV4gYdlrRbAs3pOB4nFAaqBzOzyeW/57zHaYxqDrMWecMt0u38k02Jh1pMRt6ph+46GxdO3j8h/lLR50amXcynA9k3RvyMiFuPGthQ2OVN5xRTe+hcQDwYpneR25xyYh71oG4JgktDC2Mro7WBQnZdh7DJbgd0c5Xz9rHpEH66vz9SBxOKzbcmqzBaQF0nmsmMlHfJl7HF0RYGNpKF5A/mIFP6+zhtgtDGjn+QB3FECWb8ayteCTF8uWN5ZxnDR0xIvgnB+WaYYWThLlL7QEPAO8fEa5sD8ibWBTWsIyWOzE9lNm6iYkk0SoyLJmOaDN9qrIx2s8UVhewYOHxJHvzAcg5ymDl/jggaW8PrmVtjO3wKS8d34vI4dxuoAb5Wd5MlDdF5o1OK4xYXm3TDpv1+kynJZB2GPC44A8/u76jVHGrh57Cwdctj/z52XlC6g71GRl1oai+8R95muWz8W341lGbRV3TM/51nczpUEXpiZZONwgGmsP4KKOYXIzwIIQrVDGCRyfO0x2Og8dsjTr6vSTIK8yWnjcaZ5cP1kEBuykdjv9zGqr81nBQ2Ofa2SAT3kcdpdE/NtWMEAU0CKsQ9vTZ+zwZWEA5WIN402kTo6YjsWeOmLl865tgBxwDtiH1ljfyFeANVZ+/kJPFEEmj/OZ+3k+oIqSYxNYfke5pX4mby1LyJ0VJcDI7moUjleUJS/zMbYsu18pJ/nY55Ay8UJCG3KS+ygNr+wi4cExRmzkuP6WAQOhZYjrLcM2cJxvhT35fSu1Auj6LMvdlh6TGFykFcZhPKxIgu9DWyt8yuW+nz+lhZs9vtuT8XNuv8eR92oR8l2K0enmPgR+tyVxUgornujv3rJ9ueyz1rveDiU5MagAG9KJzpVW3r6GJr+lIBfgdxm+v+hY4ZtFk+cQbGmQ4Fn3Rx+50fXQ9951ze7dlEnMHDoDqInr5zcT/LyOEho4ioJNWbG04xGwj8QTtEzSolxotyeKmV9I3Qnn5NkoFjaQ4bEQvmICO/f8Wk68EDyaPB9lkjw54A5llntJTNyy18EK84Fe6MNSVIAbhcPKppTPSaw+LNBHffs6/Zhn016ObUnYi7mQlM0+B3Ysu3+Rj5NBAJ9P42bFpI0Rrq/Lt/z2Brj7lIHpMsYs4+j0nJWJv5PH5fhaKxvabEu7jbAuc5Vr+vp6YwfPrzY0jU2vN5Vy/T5ak46v0ExaExrWgm05LO2V571ByGXbOrI14g4gretMVLJN31q6mbaAz0xuwfOAsWCRFj8a9Pndm4Eo3zzgOYAMYeP6UiT2ANwOWygAbVtxBovFA8dbiYf7oLqssQ/vmdC18sYycUgJOUhfxdpOG1kBxIRq/thY5XCJV8ikjgAzsXnA2GcjEYoBhKxMCLFk7oKEpZ57X/VVX3V9LiuKUg4T4QFfNojRLx3OtBXc4cskFBShHVb/IAc8k7JCKwsn8psdzglh5V4+uZ/EyjsUHeVbjpFRhz9bfh3mg+YFIM7fMtrKxgYJZXtsnVLf89hpZdPevfGCa41PBvimvfPZQ+72Na2rLNNkWhpz7uPBSsvTsUfa/ZRk3HG6OYewtN0CpdVBXF/xMcfF2w28lawQ8ok16ZUJTXcDuzukhXdZBd3OFqyu65ZiII954zovl9sKzMrZitXlNF0noW0and+WD/MH9FVAJxu0ApScOZTrsVADVLFQURwBUI6sBpx5N3PKxgvgqAjy2tBgktTAytvK4m2w/DW0JLEW32/XA5hTLrS1+4zREtDN6p1Y4jl2Gm+JlVFRJgZv6HKIi/OSoJ2dzJ6ch6esVuJojfyhMHxQot+XkPLiIcBPeMm5TQ7ntfJ3H6/d9AucTuPFMuXQybrf44bUCuVUt/N7vBpDWp4X8C6cehZL32kpIJLnBFzHfcDfCqbLXgrtlBZOrTJXmquMrP3bMm8t6XvdAUltvTYgUybJguVkwTHoOlZqQW/mN3MsAB1+aeFZYSu3053W37GWzV/A0XR0DNcCYivFSoHylhDbNXcZHd90udRpl9hhpoRUsvwz1mkmg2PpU14UA2UA4PnOWTtY9N40xqY2vATyJqEc2H1rGr2JCn7iZeQaE9cANUtUo7CIxfuYDCvnfI9S+NZv/dYnL/Xh6G6UAyuYPNnuc6/yx+R38kSRoBQMYMiqj6Fm4yVg7/5CkeWoEFZcEZriz5OPXu7MhDR93oqijY62sK1gSPYeeL5TKxC33XU/99yeo3RaRo/HTlvKTUd/N1i3cnBaoVb3H/xadXV49+5gAHLtlgHnZ7s9K6yXtPqkFZrTcVKZynuDF6Di602gn29ilyKx65ZkJnL0NSDSYQnCBlZInxgb3CjDA9KxT+q28iKt0Fm3zwJlIV1hL1IrAcpdFn8L1V1ZYJ7joE4rStrtQd99B//gOZOXSfl84ZXdwwm3xDqPN0BIgxVExPuxfvNHOIdVPrizvK8gz5AH5cBeBlv39h5s8ffmRK/WQvEyMZ2yQ7fPVIIffi4T19/0Td/05Ljq/CYUFSWRMpZRQN9hsRs8vOsX/qMQoMW7vimLMFC8lvAqfcCkN21mxZ29OcqFj23VNjiRbAjxXBsTLcstm2uctLXsay7LZbhvW+4X/01/jyGS8chj12Vafpg34n73ZeMfY8xhTivhbnfT2bzoe48OhqOfsUHosWHer3Q8y8gCbmuirWk/02nFqim7n115lvaG0Sgk5hE4KvlE21JEHqy0dbl7tAHB8DM9qNr1tHXR4NudeEpL2J24ZmvXlqKBwALUwkSbEGRbpTn/P/H1vODlPe95z3V5ZsrNJ4e3scELJZFYP6AVwPrSL/3S69wD1jqhlzwXhRDQA7g5eZSjSSxHXk3mV3t26AA54bpXO7EHgSWrHsS0/xu+4RuuR1OjENIen0vEyap4Ju1Z0Q+edGZyOb85gpv9C+w7oC8Ie+W5D37wg1eP7Mu//Muvk8s+YgJeEA5rGUcOHFbl2QbaHrNtPCy5cxmtCE7yDH1+pq1c521ZbVxoujz+fL3LtkIgdXkdBuJZR1B6DpRE/6L4vSDBfG4a3V73zaJ/4fDizS3F84TevnB6qN3DBjYE08+dwkhdFve8CgmGcIyxgdZhh1h6TPx5w43dOTPeZTdY2grtkEsrwwXSPWhOGtmCaF55gPbmIoQNQWwhcV0kt4Hrrrcto+4T8maJZoAo1jKhIpaK+rBBXt/oCV3W8rPblrCJY+lMtuYTBYIyOllajuPzPmTLBnzC2wGYUSABdb//AOsaAGFFUOZLUB5ZGZVneOF9vCS/OznJCygoz4DhTWsAiuWVk0rJzyqqKF5ewMMxG3mOUFmeJyxlJWmjx+OO/kf2ut+Rn0ePdkinx1SPL9fh55x4rg3EVjRNb8twKyFfawzq+l1vl8vzJHv/3O+QETJtpYwB4x3YrmcZiysE1Uq6+eN0qmPddzquMoKpK3ZvohrYOpTUDHb5rSi418BvocwnwM8gx9LjHbLNOMrtuijPzLF13UCK1WblZ55Y+ODfCWRXe5M6pLNo7jhsdyz3vWOZ5LyA36IJsImy/cqv/MprHDy7dXkPMBYwK13sXQCEvHfYIUY2kPkoa/Kbbt4LHH6zdBWrmL7gOQabZbD3TXhgklC6TPyS13zheAsOpOM4jbTNK9wie/nzqzb5YyIYeQbMqaPnhgDz8IoD7DJvkL/U6/ANHlDyJ5QUfnPeEvMn9HsDhGW8PfMG/lYeto4txy1DTgv0Guy6jC6vV8f0+Gp5PimCde/WMyva0XiInLldzN2s0Ge3t9u6FMFSzCcF6vvgaeNNp+Mcgq2DJszM8R95bJkbuLo8l9FWSX77VX7eIdmhAb/sfQl4fzYzOmS0QN6Dwr/dQW5DW/cL8FsRuRwAz9bbcsfX4DatS0H6u9thQaeuWP05HuEP/uAPruCEV+ZYOeUHfJgXSGLDWr/T2O11e4ijA9LeZUvoiPwGfPOU9iUZmGkjtBCOYi6AUJGVLGEWeBHrPKGxKAHmTADoXIsVz2on2sh4YPUQR3wTZiOcxPwEL8hJWTm+I8/nvKJ4Wch47xamzeEd76VIQn588J6T5cN90UrBfdT3W666/Pvq8b0V9+5yHT60Vb76/wT4CwOcrFAa67o8xkrjTPOh63Nb3Y4T7Z2WwvC9Va+V+UkpHA+36z9bv6vjWkO3G+qyuqOtMGy1AyCAAZaVLd9HsijtUXSd3b6m37Q7HNGdZSXH9dUBvmZa3TH9bFs2bYG1QJtuKyF7Odwz/eZ9Cz4DjnDPw4cPr8CVGDaWNCCLcsDyIHwEwDkcErAitMdZPknsO8BT4JA4LyUGlPEMWVmDLCAXWMX88ds7n2kfu5Jj8eONMH9B2+Ajb0HjOAnu00dYfrnHMRvsv6AcJiWZxIY+ywAynjISjsrz8UpYYgrAI3vwI4n9K+ExYVYm5p3u7u6mMQAgW049Lg2MHkvrs8eL898HxmuMWWG0clj447HUY+w+4GYskIfv3uVsL33xpOttus2X9hQWrQsDTrT3/VM5pz54SiFYi3Qhtwgh2dq0AHSjPQgQ8o4VE//tjSzO02/PwjWjEyzIi67+XMxaHbsshh5IPEPeNagsWK2AekBwrXeG3upgD5gW3MUjlxnQjUIIQMUC5i1nABqfrH8ntMLkO+3jPorCYJVPvIGU5/mHDgehNLxKCaC3d+ejG7yxjaWc9AVlQQ/HbrOEsw0i4vVY8hxqx0a61J14f5QBE4lWGihZew08xxlGHBseJZwQkF/haS8GntF2lCjlo9z6WO7+9JyLZanHm70zy1CPnVuAdQuMTeMCz1vlnsasvdCm41aZXU4/6+tNr7HM13pOY+HHqrNpvqVMO/+ztsvpqBAsECeA9DMvvfT0uTrW0g06bQ33H/FeJi/dwbZ4k8J8PAQ0eYN4C4I9EcrwskM/14J1qyM7Xu9BR1s7xOHk+hn8PUBbMCxklAEAAWDuV8pvBZeEt5XrbMbiOISAZADL8yfeb+D+zm+DuS3tlM/LWaClaeCPBPjn2SgnKwmsY/O750UoH2XFrulc87wQsmve07Z8d8gJfrK5LNfCL1voDnWhRC0TKQ9vhbBSQlPxCug3ZLOBBYOJN7AxBvAyOjTQQOHxsADKctpysoyozuPnniV5LDaNprNTG2k80/R32/1s03EynpZh5TLAAPptgTv5HFLseo1fXdcJkxffV5v70+lBE7vCGU2QLWHApy0LA5kFEoD1XIE/7TGsBjqU48Hvt1dZcXiTmr87rGXvxMrLq55IPanTjAVwPIHUQrPi5xY06DRfT8p58Rb+wxc/D/+sVMkLL6+CoeWQpofNUtRheem6uM7zDrWQh6MXKC8J8Icu+jJ5CblkopVTVz3X4HARNNMvPn3UBgEb5PLJ3EA8AMsYoSkMlfxFOWYimefzx6sw8V4JA1lmoZv3OuRayo8iiFfAHA2A7/0E3mfgfoFG2skzzFm0x2+ZsYFgWe7w0RrLLutk5Li8HhNt5FhuTkqmU2OVx1eHUm8920abZWR5SEthwn/XmXu9QMbjkT5tI7X5cAovNa+c121cyqXTg2aOJ2f94Or8Zkbn6diZifRvPw+DYC6M93O9Lt0TcvnuDuF5nnN4or2Dk2XTDOe+wxRWJOaLB5sFqOviN0KLAPVS3pNCcJ0dOvGmNdPjsgA0wg4BX1YIAbLsAva6epQASzi9jh8FwzuYbcESWmLJZxJWd64D/B400BKAS4ydHcPwHwCk/fZAzAMrpdyLNc3+gDwXUCc+zyQ4bSNck7Zk1U9oyMmtKYsl0PQHNKAUkMXQz8mtmQRmo5n3cDTY2DPIbzwK+gOeW5lSt9vrZAPD4VfLpOVrYQHX/bnS6brHfgP7syiFrtdg3uUtT7qVEPLh+YkG0xMQU0dHPxZO2tg8Gb8N6qdk5WPDyrxpxXfqq5vHXy8mYEk2iJu5fF+ukMvv34BIa2hbFrhiPNMDnoHh5XYGBwDDTPcGIAPwEqoTj6wQkp6vDTFL8Jq3zRdAqJXz6Vm+2xpEebqPHj3aKz4AvIDUt3zLt1z3DbABLYnJWeLVfA+vASVi9uadJ0ST6CuOpfAzKCSUDuBLaIgQC54BioC6fdzDkpEkymLvQX5zIilHR3i+gGccjqQNHPKXpZ5RLFkZBLj6fQRMqDPnEh5H8bCxEnlbsoL8Uq69P092QjvzJPZUyddWInJrhdPyZblpgO7fViJdzi0D0qC1gNb53K8uh+faI+h67XXaUOtx6HvIusOlxhU/a0PWCsXgbKW/PJfmj9vf9S15WfxpfF7lPxUyaou0wYLkCTk/Y7DusvlskKchl8v/Uji45TTQ+QBJJiixmOwaY3m2dgdkKde7Q3snJ3naynYy3a0g3HkG9qap+WQhMW9dpj0TP8f3JIfX3LetdPwdpfnCCy9cv+eoas7dD4/hHSEVVvh49Y29DXjnweg3qMEHz1ukbMI7gDf9HOAO+IaWJMqhXOQPBQQgehAY7P22t5QdQGUyNuV61RHlIVdsIgu9vIUNJRBZZMUR7zNgg5tpTj5e59nHTEC7vSRAhBVFlim8GyaWve58WaE863Ipy7Js3i0jkWRD6wQ4nU7jwOkEfqffq/6+buBcxqpDqrStrW/++prpal663q5zKdiVZ/HddNPf69n70vHoCjOBRpysUWvkFqwO3TSDrXDIY/Btq8PWUlLyBhwyQcgqEbfFTOLPys3am/KX5dK8ucW3tnS6o+8rx9bH0vZeEtl88jVbOBamnqMw8OR3wjG//Mu/fL2WSdKsg+eIB5QKIRCvnc+zPkmU8uhPFAAgaUWf5NVKgBmrcdLH+Z5+DtDSV0nJ6/c+u254Q1l4BRg0vE/ACsdzBj5FlHkMW5b0R+4nhGQjybKHZ+t9Gz7uAiVqwFrWnPubN7shJ4TcPHm9AMlyank8AUbL78p7d7cnNVu2F8i5jlv3VplrbNpI8z0rQIxBGyuk57UxEZptmDVtSR2GtdJputqwI78VsMs4JY/rTvaCnKdp6HTz6Apb+w7/tJZtLd8CvLS2V05YKdha8WA4CUOuJx7rF4N4tYjLs+LpjlzWNO1zuViKtL8VoK91aMqDc7mpBjTyUB/8M7gZONoqtzIGfBzGIq6NV+T6YrX+5m/+5hXoowy++Zu/+bo5jfcQQDP7RDgED6BF0DsOi5B6Y5X7ltNO85tD8fIXekIrr93kHCTP4eB1UH4vJrDyMR1Y+oR44B8KgPCYvUqvnkpi5VHuEd4ikZ9wpN9oxpwFzxog3G9WHN2vtM/n5NirgAaUjq83cLpMt7UNJiePy/5OPcaRpt1jYAEinwsDltKznJluPwM/4GtHRcjXgO0yeN7PuUzyQYfL8bhY5TauuI4F7I0l5jOy0POIpzTnEDrGZmL93UJiYttKdyKPhbOBfykYxzi9nCuAwbn7HiCUzUB0XNtKrDt9CaO1/7JiWiG4c5aVcBLs9b0FZtVNHe5wrGx4hjXkOsxzD6okJi3ZaPb1X//1193KCYtgxWNlEYKhbE9kthdAWMdgke+5zj4HLHfmCthRnPg8b1oj7JL6+qUzVkxJjrunPLwJx+WRE55ntZpDXvnjOucJsYgBpWRl6F3OyC3KgGTvhDCV+6bHx7LqaAPl4bHhxbWcWRF0aMSyZUBdsrfSaYw0vY0dp/zPUm8bnQ4Trmf9iTHVyqPrPfGo87SRt8ozgHd+G9qNuaushT9Ot/pwpakQTtqpAfPUWQamU0O8cWnFS/u5BjAGfYArlqsB3jFngzkD3IqIgdrxT1sSSyFYm9s66XaapwBz5zkJUlsv5LHC9LMOYZiXXO9ln9ThviMv+wgCxB/5yEeuR0F/4zd+4+U3fuM3nuwnAPy9IsdzJZ5IZjmu53kYjHmOHc+5l/LSp6knz0QJ5N0EhHNQRNTnCVMUSh91QltRDnyy1t9vg4MfKAdWsbnNrXjoK94/bU/0rrwjywrtZelqA2r3uWXSnisrjnI/CiH8ZDc4cz20a9Xh762MFhi6rL5GOabVsubyLAuuu/P5ftfXoOex4GuNS/acegx3ugXyLvdkpDXPkBmu+X5SRzLcjs7b99s4A+NOStzpuDGNBrX1bNDshrR2a0vfHe7jBjpMA7PoLPLZUmMQeKMU5XppqWPtANAaVGsQmh8IC+7f6piOIS6v4tYg4nsLr+nyNXe8lZ6VqgejJ3IBVdNu+tl/kL93vetdV2Xw1re+9RqyyZHQ7gvqZXULdBGO8ryPaaZveA8A8wSEb6I8OCOIF9JYEaMMLBv0qekz/4jhozDYVIa176OKk4/r3iznI6aTF6/DO7ktd+Z1HzbHXgS/LMjeDe316hbkHM/McuYjM8hPcmjUANjytoBmgdZJlg3eHkd+rlMrPmNF40SPqx4PTYvr8PcVEjvRt9KpHR6HyKl5Tz74Ylpb8a02neho7DBNJ6XeaXoIvazThS6taEbcEhDyGFT7ftfbA9pCEgAJQPnMG767zF4b/0jWJIOP+HO3ZwmLO9zXrMhcluN4FuZTe93OLs/3ueYltx2nd2gnyUdK02Zb8VjBLAcNUOVQu5/6qZ+6KoQXX3zx8s53vvPqNSSxzNMvdIG3lOsdx7bok5djyx3XZRlojoHIXyaRvRfCRsdzmhwEXN239KVX9sAvTyqz4qh5yvESyAK7opmgJmQWxZX6eRObN9ZRDm1PwqChPXgWjvUaQNayR+hxSJDd0T4OHDmyfMGXHquWWWhYY/sW2Do10HtMmT/UtZZs8vukAHo89LPdPitRG0/IZU8gI2cuxzzqvDYWnI9xt3CD+lf4uvm9+tB8BV89D7qiLis9pRA80NrKMhMXk5fFSh6uL6XiBhHWudX50OLYL/XRCX3GDakZ04BqWsy8VoC+t4Sw6WZwdijH5cNLD57V/hUfJX9PHsODkyViYffxH365e4ArL8bJvexP+I7v+I7r7ygF3l9g2jwQzGv3BcsvSYQ0AmbxCKLoee8CoE+MPgkvgD61Z0GfOpxj0Og4fveBN3yZXyg5ThP1PohcD71s6GNjnc92spxQP3yGL6a1l5miEGk38w/2wPyWO3jjNlr+TA88bNm3p7483k7mcxtVPYe1QOlW2W1dGyOgs43GLtcy34qhy1w419+t8CwrbofnMLt9VhZd7+JlG6QLZ8wrvlspn5RB0lM7lU9xrxYGE+hnzbBuPI3t+FiXv5jjegwADhEsV7vpsDC2xUCHLQ3c9Pian1mC1Jq5rX7zfHWy299tab40+C+Fbfeb3wBOgIx3A6AoCYXkxNMAzrd927ddQ0hZZpnXOvIeAP56PT3tI+4PuBOuoQ/ZgRyvIBZ3ngv4hk7H75NsYXsFj2UCmbKihz9eiebB4xVB5iWACA20ifZyzlPmOqz8kwildd8xf9EATB1tJPX4or/cXsptmXH/d6jIcrLmrJCtW7J5a2ws63SNz6bvlLo9Ddad13X2d4cyV3lNq9tgcHZeGz4rTHTiYSvKxRvXv37femZ5NJ2e2pi2vt9HyAJyM6s7/hTX5NnT96a1wwMOF3kwmdmOQ+Oiu132bFpwVqc5JsszJ8Xoz1VeC89JuNfgPAlSL0912xsYXEbuB4x5vWTAP9/f+973XkE74aM3velN1zeKxTKOYmAFUMpnApkVOOxATpmEYADShP7yFzCNd5Bns8GKlUOsILMHkITFTF0OlSy+A/Yug7g9MgT/CcU4hOHQUnssUaL5nvBWNvOlrJxNRLnsu4C/hJx6kLrP24tBbqwEk1CuhL84ppu5ERs9nQySVpynfP5+C6waPBcYdT91/feBoK+3gll523hact/5V/+YZ8aoBvh+Djy6L5EXIxf+QQ/13tcPprHpbQVHmpPKTTQFtZviDmhmu2FNVDe8FVG7OItwBkZrbwv2LS0Kk7GmTuC/FNpSWoCs6bWFtNrYNPmzO93XOh+/bw26VhhYuViefIYur1jBsgfA8j7f5Mlqo7y0JUohwBfFAO84ehmQTvIEMDIWBRIFwLEPnJVEyCV0ciJp/rz5jfh/aHXc/CTs5kH3pfcgAOwf1ymqKArmDAgBtOcR0M9xH1GQH/rQh651RQFCFxPaSR3X7f62bEIP/cYfitJKDP5l53RWiPlAP8t2j1eHrAxIPLPSCYha/m8pvZb/Lq+NnlNyiJvfDdhWjI1pS+E9S1t9n/LdZur2UvhTW8wLe6aUZ0wyfiwetuKClpMiIB1foXlKPaBupQbgZoKt6zVYeXZ9T7I1aKDpuBzPWrCp08sw+W3BMqivsm7RuzydxSMrHIPE4rG9Cb63y2phWGUALPn0vgCULCtpAMmcu8OA8wtwEtrJPEKs+YBQPnmBThIg6VNCOXoioBmvIMokSiZAihLhlajJ6wELDeTjaAgfaNhK2BZgh+rcZryHlMsENDxq79NHWaComG9gZ3c8pigF3rJmsPCEvvvefYn80BeWSU8+Q3fysqQ3y06jqLNvg13dvZpmWdU9FixzLb+WsftkmrHZY8qAteS1Qc/GWdfV5XQbTvS2cnS5neek+JqORfsyUDrRp/57Fpztcm8pzlYSnW4qBMDVccXFwBaYVVnP0ls7Nxh2xxjkPEixfkzjWqnhMtrr6Mkoymh+mAZbd3flWUAzVretL4P9gwdPv/ITq53rPQCar0t4POgI1dA/tlJMA2Vh+cJjLHqOmkjyevY8k+tMAlMG7WzZcUw9KUrkDW94w1XheDKcCed8omDiKaTO1M/ek3yyf8C8a/CHXvJ4OSz7Dji5lfBTrrMyiHdLk+ALIIrnQhuiEEL7+973vitvDLTenAZIrv5IMi8tL/Sh9y0wHqJs411FEUUp5DdLUZcS8BiwQrUcmac2RjynwrixRWx5f5a06uX7MuZOQN3zLfZ+XI/LPdHp9ptfpMYW37MxY4MCPht7Fh+MkY1jrURPirDb5/nD1eZ5lpEraoYgfDB+5TPRXOvO6HhqKx0+zdQWBLtVtpgMet1JrXEtaGtJ2NLU3VbKMei0JnYZ3iDmSdAWNMpdlo7ppI3LYll0IxR4B7nOah4mQNl8RlmhjxVHgCBHSFAXr6REmZivWP0BK1bgMIHNKhzmLbC28U7wBnijWJSBl5h2+9w3Lfi2qFkdBP+9Szlls2fA75CGD56IT2LpLHH7PBNQjqLMHxPzHEjnPSD0B3LbFjWKPZ+px2Eje14osTz7pV/6pZeHDx9e685igB7Hy4jBW7L8+xlosfFmwPX4XGFn8hiUVhij+7Fp6DzGpGU0dX4n00xqL+1Unj3ry+W2h2E86vurzp7745kl0401rtfPub13z+IhLOBeFTY4NxH9rOORCANAZOViIfGAcDm2qmCuNV4P9KYPZlirL0bd17ambVkk1uoWHgDppDC77lPnmaalhFtBGXh8z5amgS+fhOXYrWuw6r7Mp8/1p2+oO+AOuAJ8AbLcx8r2YGDSGesqSiFxcU479W7klqdWmG01ISs+W4iNjuzSTn0ovHy312IPwSey8krNPB/wz7WEj0IvVj0GDPznDWt4aL2HovvEcoZRkRTaUxYv6clCgMzzZJLbGw3hQY8r92vL+wnsnOhT07yeP6VPRu6dGqdsJLVC8Jj3uDuVu3CAZ92+pr8NS5fn512Xn+0TDUzrSek17X2Pa5ajTjffh3CqbDHilI+/tgQW2PIJ42BAW/1cI58BrSdh+AMErHga2P16RK9hN41+hgFtwaPdPaiom4HtZz3gDcwnQVrXT/zsflmhLvO7LTfAGIuWo5VttQIyuZc/FAL9Rp/w4h2Xi2WePAFaQkBYq/nkRTgBVXamu2+eG7Fl98Wt/mNCmfaysYyJ69DEElcm2913lIVidJ28ByFKIkoMxQfdTFLDb57v5bv0W8uyFXI8q3hOSewn8WS4x6yNnx7LHXaz3PWmT8vZo4Oh1LK7+qJpWv1ocG659XVjTde15kFO46RxqOlcbeU509bfmz5HObrtjSvmqen0vabj9PuUbs4hLCCyhb4AtpMFzEzo8vr7rYGMNWPL+0mDXtk52taB87tuW48AdVupy2JyOnWErVasOPgFCHpy8pR6wHTf8NtWGbxvoDd9Dse1QoM/DuXBW1bLoMR87DKv3Qywsk+AMFQDKADsPkSpcMppyohXwDsKWAZrWtd384Xr3hjoPme1muenmDdBEeV+6AHE85tlpygxgxNhMpRCJuDTprQliaW1aYuP07DX67AibbGxgmIiHJdyongI/51Azt/5bVk1f/zdS3G9ZNt96mWSXbfLuwX65DOdLaNOlmG8z6RWAB2Hby9i5btVp6MebuMp3TIuGb9ce9YyocXl8tmYZAP0VO58p/ICPH9H4BGgJJ7rgb862YNv/XXjmkbqQChZteHzaRgsrWSsBBy3df2tKCxsLSg8Y36Yj1iABmfoRfl4DsFCcQK4/u1BYPqWULgt0Hm5XKYyMZ+g06Dlt5MBVoSNGnh4PlYs+wvyLPMJhGsIE0F3wI15A840crjQdUB3GxieZDQvkB2AAUXNctMkltAmAYQ+aptyLpf/tRTaYTpCTElMWGP5ozx9qiwKFDmhvdRnY4f6UEhRPCk7SgHDh9eXpg2WixOIWy4skw4p2fPzWDa4OhQLf7zAoeXT9a/5RMtm40zLWhtwJB8bglwYt0h3wxi18elxQVs9j2n5cxuaDtdNfni6jObnnntu8swyyL1WKAvnFr7O9yGcLGgzhtSWuitbwNkARZ19r7V708guV8oBYC0QKAi3C5qbpqZ31Wlvp5VFC64Vw+IraQn36qhOS+iXxbEUm5WClRz5AR0f3YCguh1Y1kls1MpzHLpmJUx+lrJSVwDSBgR7F6g7FnVWGAXkohiazw0S3Evy6h73uz00wJ/yaLsNDrwSFFg8Bu7DG8riJUKMkeRDwbm9D155Gxugz1yJjRh7aN2HLWsoau+cZmkviwDYt9BGwQJA/nq1kMdBy2vLbdezwL1/2yDtfL7emNR13xrPxi/Lkctf7ej85ovHOb+7/pZPjzmPK3/vv+XRNM39nT8r9FM5M2S0QHsNOH8ykcsuUAtOM9Jlnj5bYJo+Dl5zA/neHbjKcacuxjdD/b3blWT3E+YzuUjIhHp9ro09B1tGt1IPDA+KkzIxbVjMFgpAzQoLi8X94uWhnrtxvbZGe7DTP4SfeNk8J54mb4CfCeSPfvSjT5aX0s62IJes3DcQ3U+ECq0kqIt9CXgyf/Znf/YkFIa3gMfDfohHsj7hPfJAnxOO8pwKE9zIMv0DXxhbNjhyj3FgC5O2hYesBrtlZJmfHhNcW+D66NGOqTfvreTM/07tzXdd69mlHJxW6MXjt8fcKuO+5DHocXzih+lYY9krDk13K+a+3+Xz+7kRvjvhxNFDsGZvpt/SylxbGshC2c+2wKw8lJvrGTjZzJQBFVDpSZmldR1DS/Jk9bISGjR9zYOraWfVis/M73YtNxuL9GQ98Wz3Rfed6aJ9pr9DLlZKLmdZgjzLktVWgj4WgglUQBDAsmIABAHF3IsyYCURZQLYWPRLyLu/zRc+UXxY+LQJL4B2Y60zfwA/A670J6uRoJuJaLwAJsrtEfhUUo76IMSWevxeBGTCysByxRLf/KG0kliiy7wGymfJt/sVPjbfLCceXx1e7vHr5x1BsIy1LLd8t8HTdXWoc313ue2Je2w2TUu+Ws6goU9NWNh1UjbL218eBnn8STrJ/q28K918p7InPFdIA0HCOsK9xIJswIPYDkOdgL/vuyG5z4tAGLSEjchLvi5/xTp5pmP43fn+biFx6MxAaaFDaVmptBJtvnbfrM7t7wb+bhv8N7CaF+skUPMeGv1OZaxa88b9YJoBRCtL4uhMGsdD4DiKAFtCH+3mQs+axGxjpAeReQF9XkFDHxLicniIV3t67wTx/fCE17mmrBgsPsOJEFESoSX4SNiIJaj2NOxhwqP88WKo5GMzX8ZEFEPux5tJedmglnOifP5Ry0zzZ41PrN9HN4zCU7ovJGo62hB99Gh7gc7jZw2mYBLJcX4rKIf4qK/Hxhr3Tv5teTTuca8NNeowBnTbWiG2ojsl2tq0rX6YHgLCbgagAbuTADzPN3gpnQloLdZ52lrpSZx+lrN1YhGxZhxaTQNC4olmOqStHOh0p8ETygYgmme0wYefJTExaFA9AREnYPayw5OlQ/30Q/PJIQQUEXxBecMTr2aBXtNDu92f/s5zAOpVwF4JDTHnQ34sVuYcWGrpsEsUfaxob94jLbe8gQTw8jERppk20g8OZzEBakXgNkVRIaPE/xPeynERURhRBrzKMp8PHz58EvpKYuMfy3g9z8C5Tu2hMi5Sd+57Ijplpcwohuz3yIY0lFlof+Mb33j9zJ6INUfhfvdvUnsQ7e0vA66fX/LXYRD6zXlt3Jk2r3BrAwEa25gj2UM0LkCneWGZhmeNSd5J7rmrHrNNg+Wyd6qbn/C4MbXDbK3MkVdjjUPVnaaH0FbA0lRtTXhAtvZtK4OBRuO6PhrqRlE+4IA1m4HHQOPclg69tJfSisdC05oZMMCKBcg6/r/AmdSDx4PDRyDnHmDoFRnNA/Olhd187Pi/486O/1uRGCTd523x02a/XpLyOdvHIRm8Acr0M1iuTHrmO+9TZrew+8bJ7Xb55k3ntSXm57HMPQ/mV2KyH8GKI59RALHGP/CBD1xXQnG2U0A44Jy2ZtfyF3/xF18VBpPOoYFd0fYYMQxQTEkoSjwIZIEVUCiW5MuYgMeE3JI39CRFWcADA2oDbo/zTjzT1md7t+6HBZAep07df/Rd09AAuMB40e/n25CygqDMHouLVy1Xqw7T2mWc0unZNoIaBzr/iQ+kp3YqI/h9vUMcC6RaEGyVen1/ki1TiDbYePmVV7NQD2XE+spg47RLLwFcbp5Br2knjxUFCoE1595F6jwWCCwXA7PdbSySjmX3UlzobvqXMFmB+bp5thS75zLMOw9Yl+tBhrWL5bwGDIADMBHXxuJ2P0MPnkFWF7nve9CsQWmPqHd7ks/9a2vU/EHu+Ot3EiPPhCkTvkmenM305je/+XqeUe6xozqWewA5Rkva5eW1WPwMbjaW2er1pHPyUC+b9vJsFBHeQpQCy13ZzJdrhK5Qbsi15b/HuhWo+xh6DUoLhLqP4L3HWoP46Tn3563QR1KHj7u8DmPR1h5n5OvwZCu6Ng6dFpi7zlvJEQk+l8JxuTbs2gvr552eUggRRt5ta6bZ4l4EdAffp4nMBDOS5xgUnjhzwurOZwSdZ2P94ClAjzt5WSYOebVV0aEhD4IVt2vhwops/nTIpjX8EhILpfuhQa4Hl/m6rGgU9clKsjLL914JAx8AK9fp2Cz8zXfmMPAmkjhtNICZODxgiuVuj9I8sOKy8m1+8KznIzzA242G36GBCW5CXCg4ABvrPfH6HBmR65n7iDxyJlNSZBVvE3pTdn4HrOEJtCaxoi51whPmCnIv8o5XhacV5ZAjsHNsBWc/haehI/WkrPw2XxocLWPNR1Irj5bXhQMnI2cZQ50XmpLaAO3UCqrz9hgzVrQs2JC0cjSdJ2OFvMg+z5BO4RsbzMiZxyV5SCe+5rk+yPKUZsgIq24RakHxb743Y9r6ILmhrYFzD1fd9fEbEALI8h2lwCoP10F+h3haebWwAXxt/ULHiS8NxliRHQvGg2jA5jvg6tCOrbNTf3TfrPxY3W6H6+gybL3SFoMoedip7JRyeI2klSBWKrKGBcxEMhb5iu12uy1DtAkl3wre1iHPWa7MF/oAWpLs2XhzYfKz6oh9E4R8WMqKp5nfsdoD7kkolLSdFUfJ4zO5qA9lRNjJ+wtY5cRGNcCfvuTd1LzoJ0qFiWmPAcvNAsaWqSWHDY4nWXQf3prw9jUbVdDY+dtSXwaTZYJ7HdfnWQOvsWK1qZUXNJPX0RErihUya9z0JLnr73HbbWserutJT+1UtqXYD7bG7ms0zjHyFfOzRWoL3PFHzxV4IpM8gBq/WeIXyyhCHuF3O0yLwcXC4Q5hQLqjbrmgbj9AQHgEsKS9XKce19uemHl8AjdbHvCoFamFmmc8Ee4+AQQIK9iLMX0kwirsvE0+LOB8Jyzh+rF4USaE/BySswL2fMSl5M+gxYCx/NB2QL4HmAcdCyr4jgJMuziKm3GSclF27DeB1gB+PIS0M88hr8nPWU8o2iyM8NwEMoOsoEwcbsNDgW8cV+EFIcxd/PEf//GTa1bMbXDQX83TBnR7YPa2T0pgAfvygBvM+H6yhheArvHeRiTXbHx5Et842PiEzNwC1dVW86AX6DT2OJ1WbHLNZZvmVnQYzn5u0f+Uh+CNR93RzQS7WE4GrHaVIOhk6bVAYiV1nR0GAogzuJIyeYc73kxYgu467B04zm2Adl7Kgj7XRSf16gdCF/xuT8B86sFGPSuZJy4DoWtFDi/dHug1uPrk0VjwAbqOwbJKKMlzKMhUv7ie523ZOmbPbw902uj+tysPz7t/1oBql9zAQXmsEoqhwYtuDMS5ZjnKM7HKOVcof8Tq05Z4suFdyiR8xJHYrLZKYr4F3sMTNvElccw2k/OEeuFZ6Mgx2Omv3/zN37zeC71s/EPZE3Zqo8EytRY7GFQcOmrQ6t/Ib3vaxgwbJpZlg73Ltlz7nr+3YWdZSgpf2bNBGM5KwnJ4d8P6duqxe/ruvMsT7jqMN75m/vm3Zf+WIps7lTtG1YDZxPHp5VZuYBNoJUA+PttKwHLl2gppJFF3OjUrPNKhxHvt1nenugwLza2OACwNbt3RPRAct/a6fU/k8qxjlMsVbO3eQth0mD+d2kIG2N0uBgWGgsNd5hl1OMRCskVqd5lwSUCLl954+enySLp9rTCSDOqWweaZvRcrd5QAB9RhkdN2DpNLniSHhJhvCFgz/4EyBAjTLpaleiMb9OIJ+FWm9iJY8Rb6CNOlHMA79xIywsvIu7CZXOagwPYmvNS8+Qtf+OvJTYfqehw7tcHneowP7rsOG7qc7s8lC1z3PInxBfp9Si991MZGhyGXTN763nS2gda/Vx2+ZsVqbDjxgedPmDEVwtL0DU49SNtCM2ENOKssD9jTQLY10gxMgjGxjLKig4PCOlZnEDsxp72fFoaTd8SzLtvtMFg2LfDJbmoP0qXEnFqgfK3pbZ5337QSoW+TsC6hN21gqSRl41UQpjDNlhFP3Po9B5YdL91tI4WBYLlkQLufaY8HkOcLOJ47NOc9AmzwAkiTj1APgG7rHvAmvBPDJPd4Pt6rV6vlexQKO5b9fm+8E75TLnzze6Sx2nkXBQCPosj1rHqi7cwfpBxo854ZlIPPAutQivm+sKABz6kt/Pw5jIZC7XH0LJazFaqf7THkP2SaHd5WBP1JHX7OyUqr2+u613P+fmqnjUXz2uPEdTR+dRs63Xv8dRPYwN9EE1/sTmniPYDbCnH9DFaeaXevaeUzSiGDGoDxBJ2fN/MMOB2X7skcg5xj7MuiMh87HOR2orDWMtTuGycPQOejvH7W7eX7Crd0WX6rGjH6VmgcvYD1aTedwUJfuP3wBs8AOuFrh7Us3EmAtK1K5+WIaNOWfKGFt7bhFRD2oc0Avk85xfL24gfq52wjPplToA3wheWnqR8lkesBcIfkzBMGPMrLK+RyD7pRsnjJ8Ra+6Iu+6MlYYOURystHZrtP8eAC2B5/hLF63HafLS+XfvHKNfqFNvo59+XCHMq2LHc4y9et1CxLeFZc70gJ9a4x0mOlFZJpcFoKdRnRlOU8LsM0dx086+XuSxkkHV+haTBMMgDSMcvS7ucbkPhzbJ7nF6NWyMTldaNNTyyyDLoIv72Ktk7M+PYATIMneHzPyoOB8lLFMZeV4EPUKBcLDtBqoSDZWuJ3K7a25JZVwGCwwqMdVtxJhCi87NT1caidX5yTez4O28tHaZfj5fa8qMOr3uArSsXhBPhKmMb7ELCKAf8ANMopYAmAc8ge9LPjvOeC2IEdUOWAuu4rBmfq5VlCPcg/c1y0y2+Io/8JofEMSofVWp54BrjzR1goSgJF5yPL+USZ0n8pm+dZIOH3UFh2GRceXzaCknoxRlvD0GA+tBFJ8nj1NRtOBkgUgOfo3D8eowuAbai1cWZMbOD3NT9nZdTldXtPRh75mldun8dnY6fb2mkeXWFmUpFXeTS4mBCDpZlqQqyluoEG6SVc/YyZ5foY6FmLzXk4tjrawqSMvg8foJlO5bOtIj9DOV3e4rHbZiVMQrBPnemyvfu0edPJfez8KDZCHLSXUAJ5bdkBFB3TdNv8x0Qpq8J65Ri8wEJlfT7lWfEC7g7J0W8BRC/nxIrOPXYVJ2+UBSeDWo7zFz7kfp4NrbxAB7lgoBOyYW4gyoA9CNDLxCWJsE1owXrH8yBs5DOjLE/wmvbzHJY/bYxxFK8iXnN+54U9UUa8nyI7rbPLOp/2AlDWDm96pVYbWPZOrKgtA8twRBZcHrxdBo4xxbjTcm6PsQ0y6OezLeuVD1lvPDDNDfQeBy6rFYjzmy4nY+9JWbicru++ND0EW8LdcRDlM+1b87osE2UrugEjyWGXVgxtNXSZTu78rOTgjP0IHa9A7LZ1+/hcmt/g7AHg0JFBZQEj3/27w1oWMNO4FOGJ976++umkYGk/YADQJy9WMUCYxDLdAG9AML9ZM+8QgD0vVuuwMYvQHPy0ZYmSBxxTRoAUBeBVYfQXE7m8hY2U74SCUAj5HtrjUdpDCB1sdEydKAYsacundxPjaTAngeWelDo5boVQFHUC5ik3ZTx3iEcjf1YU9qTwblDsGEj5nnKj2EJbdlVzLlKO1sgheFEKHJ7n5zsUnMQ1h3LoO4eE4B95oHPJHt4pedsrbIWwgNw0GaeWAdTjwzLX4wMvy8+09wF+vqRQ3qrTuGIj2m0wv7i2lMIq25+mt9vq9GBlBsAaxBaTeO7WvEFrKhN00vbN2FsacDENmjIQM+Az0L1hrctz7M3Pt+UDSHa4wqs13G6et9B6ORu/qdedfuKX2+52NOB7vXQ+e05iCZW9DAasw1f0CRPGWIyctuljF7DoAfEkQibwkTOLHPpAefezuRYw5YRbrGyAETAFUAzE+WPVEBvIorgCgoRu8slxDwC5eYpiSDmxtAnZ0L+0AYUVkIWW1EnoJ3mRSUJlAWiAlLecsX+BfsTq9uS4FR39B2j51aX0aw7gY48ECtSrazL5HCPqgx/84NVjcDiIPmiDyL8tmx6LpIUpLbc2FnsMOMyMcvAY4x7Giul2uitj1HQvT8N5eow17Y0rfLZ3b4NxgTN5+nsr0UXnid5beZOOk8oU0KDcVjv5/dxSBg1gC9y4325mN/DUkUvD5i+DJkKe1Rxs7nEIZykT2mzXHNqWlcI1h1O6/c7fSwytpFrozX/zzTSTZymKpXDdbj6xwFF+WOUBC5aAOkREu7Hu2UnLcSNYySkDrwHlyWY0H3ONkvDRz17qivUNULKCKYkJ47QrljADkclfeB4gZ/VNwC8bt0IvR24DogHolAMdkZ0AZOiEfwnD0LbQw1wVbfDmxJSbMjFK8FpYxUR/sMcjz/gdBpYf+GI5wUMCoLDsk9IP7D1AhnMtdWW/To7bSBviGcCnHL+Rz3gMKIakDucZjPEKPNfRMnvCBxtBlk8bIassUo8Ljw3o69CW6Vtzoa3Mmsbk6wUQ7anZEPT3k6dyUgydOl8rUPN2PXu6NxWCY4h2UVcHwAiutQDT8J70tCXcxHl+wWUtsDOwNfOZoMtzTCiy2oOjE1b5gD+0mmYzm3sIf1v8Fi6Hwxxzd10e0PAVa5NYOm23QuuJKnsDi5/uwxXj9Zu7Ak68NrKP8qD9fjEMMW/H/r1LG8AB4L3UFK+L4xgAl5MxgTfACiHox3rHogf8/G5kNnfxcqVY/QE/zhMin88XgtYAdSxsFCUTx7H6eQkO8wT0b/LlWvJRL5vMvD+BvsYz8CQwISD465VEKDLqwhPhmAo8EDZv5jevJUXBhraAf2gIv6IwUeq5nrJRwg4XWs4Yj71IgdTGjL/jkbVhY3ldRlaHopBPy76ve7x3WPiWkqFdSYx3+MBvY5vzgwHOZwXT/Fg8c7l+rhUmeU2PDUd/7/TgVLEBjGQt6uvdQfw2UC3tZQu1y6DRTtbmLYy+Z8uATsmgCFAQd3a4x2cNWbgsmAi7O9L8OKVWOOYtzxpkEWBACB66LR2b5VmAgvxe9rn46O/ktwJL4lA09yvPAVqEauhzwAlQYNcnCX4T+rHFCX+IWTPosMaIqxNSQXnF0k8ZAeSAnZVXUnjJezMC/snD+52j9JgD8oQvq6qSN3UmxJNk+tgdjGXoF9SwrBOPJHl8eCR9lmvJz1JT37PcGbBQqpRrZQuv/OKe1OFjsVGKaUcO4It3gAKhDzNWMmZSZxQgyi50hhfQ53OnkMUF0g1Cy7jpe2ts9Rh06BOFShkea6aRcow1S4HB827PCvWcMPGWl+G8K7kc09PPGV87/G06btWVdHyFpgHTrpbzGVS6jP5+0kiu81SOlQrJ4HyrTAYKVlzCAFiKCI89IoSqwZcyHVYhWbBP4a4lAPntXcvQ2YqVQf4JreW3sAOkgEQPhCUErVCWMgT0k7zCh9COJ9MAIwslComyibEDgrxJLODCC46oK33lJZpcIxyEdY5CZ+4CRcF3gDzAxvlJzA/g9RDnj0XMaifi/yiugGMsa0ARmgjN5H4UZ+rxBCzHVMCzfIfWKIzE9APU8S4sJ8wloPwIs6Uc5kZQ4oAyE8nIQi+ewJNKGSil1B2exgOIxwCYhJ683yFtT5uyWg/F6Xkaj0HqaeNvAZjHQ8vcaRy4vIVH/m0DBJldqfGJMQkteEPreSueFekg8XwbYqahFdfiEXn7eytTG8VLSd1KM2RkrXnSKlS2tCvPGDwXM9ob4LlukJ+zK2hGd9jKYErHYSUxqYmF67AW1p+F0kx17NbtZxAn9SSxPSbTxzr5diPhDS6+Y50Gez+Hlej14j0h2AqgE5ZuhwopC6Azz1EU/PYqH0IaDnsQKopVyvuGWfcOfwJaAZ+AM5YusXpW+NCfxPI5VsL380m4i1M/Gbg+J4hjTgD80JDn8ods5z6eB2cV8R4ONnWhZOADCroPxENZJWyE0sMqJ9SG0jGvrVgBK3jjM7s8HniO1UX5Hd6zPJWQWTatpQwmusPbhw8fXj2D0EdILEqP3cTQ530Lnxgr+CzTlnF7Bh6zHjMN9JZfjxljg8ftrVBul+cx7TF3Cuu0wQb/O9+61vccTVnJ5XfUgHHeWOV2msetZJzmKqOkdkHaguSzLQQnu+wGfpfvhhqEudaKh+stbGaWARL6r419ZUIyFlEEmhgzsUusP7/D1taChYs2maaT8Phztcf3CME43ujfyec3emH50U5bM61YyX/LnScvQGbFi5ULf33gHdc9qUldhKL4zlvCsNBZ+UMbM6HpIx6iHFieaaXLeUIAdE71pD959Sb5Uj9nD+U75adtAbkcdZJyeIFMaEretCv3kQWUJks304aUk7ypJwCLErQnAC2Aa2ggdJXnQ28UDBPNDlX43RFJHK/QnlFPZiaPeQ3NeF+28nPvq7/6qy+/+7u/e+Vj6HnhhReu9OS1mzGkUCLQyMY8G12NFU42FBq4wATLKP3TYNvP9b2+fjJsTavHiA1TG0UuuxXH5XIOj7ldPR5tXLZXs9oD3VxfSmR5WLeUjdPch2BAhSENvg1mq1IzosGdv1U3g65BigQT/ddeimN8thZQChl8MBZLDsvLYZdeLdHC0xYP/DBvDNDQDr2O21tJAGCEXhj0WN3e2el2ngaJr7WCIDkGjGL0cx/X8RBMVpKf+wi5+45JTlbU+EhtnyFEGCngnDzJG6BKmQFlDovDOuUVlbxbOLRxYmVCILQtIY88x9EUtJXwC8s8k9i8lboIMcHr0JPEPEU+iaXTN4RtUhfhpAAsii/04hlGBgOoMUxQrKGL0AxKlcUEGCp4VtSHwkBW6F/uh5dRVOEhJ6x6ziMp99LWt7zlLdfyowRQUml38iVPaEB54zV634HHpsOxxpM2/JB3G1JtdFp2l2w34C+M6X0+7Xl7PPhZG4FObXX7u2n0uKJ9VoLmQaduQ2NqR0aafy7Hn6c0X6EJ09CS/FnIugPd8CRb6qthXW8Tb+ZZOSxt6XtmVjOOQcXKCdZqR+BZxogg49q3gBiIu23dWb5nZUCbSD1ILLRWQqz1Zz074GO++btpuLt72pvpPrRgQ5fph98ASq90MU8BCytNAycWP8tFUYCJVcdD+MhHPnItMwCOlxKgwnpmV2+eCchl5y3KhbqxXgPenuxm1RH90C+rZw4iQA1QBshTR2Qlz3lXdH4D4ikv1zM3wIvteUNank0fpnwfuhgF51AXYa784d0gd3hf3pvhtnkscswInkBoSHvwcLyvAfrSzsyl5JmckBrFQN4oShsoeD5JrAzDKDiBm8f8LQDz2DJodpjIY9FjtdOJnvboFy3ktfHJtVZEt0D/7pOw1E/1+7s9QY9R+NH1dttWO4+TylYKWFDrOGKIbAYYJO3idD1+3jsUV1oa3B1KsnAsTyb3iVPnL8sNUQh4Cn7eNDpcgoK8XJ4OD0Fj0+3r/Zx5Zf5zP/cAVB9hvMC/aW9erNCc67EM4DEA2mzsIuzQcVpkJiCX3yiQADN1Ezu3lZ0YdSxSANtr+POu4lbqrOkn5MS+AbwQGwacV5QyUCxMCkMfoEwMP7H2ACLvLsj9WM0oZ6z+5Al4h/54Jqxki1KLUkB5pJ15NnyI0mMiPfcD1MxhJOU6k7xsGkPeMAa8pBe+sqLJCw3wKvCmQh9zaeyZIKyUz9Cccr7sy77s8v73v//6PK/dzLOpM3nyPW1i3soGALLkFXS35hecFnC1AcN1528jrAG0jdKT9b2SAdhGzinvov1WOuVZPHK7OjTn8Yu8GK8agzvNkFEziZg1HXoCN3cIFZ60IkDWSqG1P99PeTu+53h/kmmGeVhYDexxq5PPp0g2o5OwhnsTWq8G6ucWIFtQ7XL389CcfIAI37tctx0e2pNynu4ze4TQ5+MRCOt4YjOJeQS8GJY0BjAI39AX0BwAwlIPGBJDR9Hle+4HeLJ5yks9Uy4vjU+ZvA7S/U18PSngyQtlUGxY1YSAvDM5+VKe4/cBfE4mZYVU6IvnkL/Qz4qjWNMB9EzKhk7mGvKZ61EIpDyH98Ex2sxJIAPM50AfNH1cB+EhJ8gM+fEiUQyEwVjFhBcSutLm9B1nH7ExLS/ayb0oufCagyNR7oS12HkOjdDWYNtj3Knxop9boVmX05ZxK5OlKEzTChEZaP38ytf0LINwYaKvgwGNEW3EuRzA37jrKMeisdNTCmFZ1LkGACQZTE4arAGnG77y27JdDW5rubU+161E/FwSYOqBg3vlExxtIZsX3V6XbU+ohWEJv9vgegA+x2BdFs8aXCnHoYQl7CdBXMJF+SgFAAdPxRZrkucCcj1gmPi5ZYhn+QR8Hj58eF3pkusATe7nGfYxUD4ritg3kH4LmLHqKJa9rXjHjol5o9jybMqPxcsAYn9EvseqDo15WX3uBxhZlUT9KS91hm4mwPkOKIa+POfdyigr3gfOPgf4j1VOSKitU/jvEIGX+lqePFaYG+G51IMCZL9B+BKllqM92OeBF83KLM6tYnVVUn6nrUzE+0yqNuqgv8dtj+uWU+dxOxvQe6xYKS1D1uOy66NPfN11nNJJaa3vz6I0+rlWbv188+KWMkt6cKrMzMW664IN4N2J7mBrOu61q+MGdiNPIGxB6voNstb4BjlbUrw/IYOSzVJ+tpXFSSCgxeGvT3zi1aeU9oSaBc8eQIOyfzOJmORzWqwsHW/ttJSyeU1/Mx9gTyApQJmBH36lDkISAbdcC4ByHASgiEJh+S9r3DORmd8BGMA/91AMeSblxELvY56Jv6MsOEKCuQz+WGqchLWeOgmZpN1+dwYhJ3iY9mR+I0orYEeoK3XnOieF8sa00JDreBRRAuEXp6ASfkW28ABRakn0O6vfSMSN6QvLhfu2V63gNeFlkSfl8xKd1BMvIO2JF8OkchQFvEiIjCWzUQB+qRFtYNPgGhseq56rJI8NsZOsrnuUuyx8z2M1Tf2866A840mnVk6+zmcb2stAfHTD42jltvCnx7A/T4Zgp6eWnbqjuiIX4oocjujO7k7vzk8yaDWR7R7CXNN06uS+jwXo71goEWI230T4OYLYVrhBYq284rfDPgxcwkEkdksD/rTVGp0ysPAYdMS9PbnV65gZvK2ILTQ8u+ZcCJ9ACwoRy5pnA24Z/AGGlEM4h/XqycNELbH6gAyhm1jmmR9gDwUhmoBRQIndwlEUgA2yx6Rmyo+yoAzmCAyySVj07MpNIt7vGCveIv0WOgDw0Mtx6nk2bYmSePe7330F0tBNPB4vB4s85XOQHLxlkjo0ozxQWMgS/WvZ9fEiyAaGCHKFPCIH3lBGuI7TZum/jIF8Rv7TjyzFzQQzr+RM21NG8uAhpOz0OWcxJeQVrwl6bFBZrgxgNpqs3E5g3mO+saOxrHGnFQDlf1wHKxJB6MPxuO/ybtF2+t3eSOOuv7dh22UtRdEKuK93ujmH0ADT91v7LEabiMX81rrdQLuUgJyB03WsRjc9LiMJK4qByqDJZwSapYYOO6y5lJWgv2mBBlYJEcv2gHBIxs+wyii/OWagvTHz2CBivgM07UEAQsSn4Rl5WTFD3JiNWgELrGAmvGN1xgonHs4rI8Pn5AsA8nYwL0/EeueF8AAeew6iHAJWgD2LA2h7ykV5o4wMzIRgvOqG8BBzSTn0DaUbGqJwUharnngnQvJGKUQRAIAAbfKknQBpygstKYP2MM/AmVFJKZOlnmyGw5IPbzglNtdY3movzvMH7ltkKfWgcAmpccBfPLa0JzQyWcwkeO6z8zopiiQhwRdeeOHaHibUmWtCZjyX0KnDWz1WbaF7jK+x5me4Zvm+b7x2WbeAvi3wTo2Bz1JeP9/l2khc2JYENvZ4J+996cHpxipwEW+w6g6zV9AN5hmvWeeZpuGkBU/fzbgulw5csfn8+Thh1nj7bVVJp7Y2nV1219ftReEAXkkcUoY3w2Q2z1rgvRa+LX/T5Uljh7Pa0yP5aOnkIfQRIAlws84fWgM4AVAAHDADwAklxdrO/EEAM8CCPBDeYU+BT6hFCbIyLImQUQCKVVBWYOxlSNkBreRjLiI0xSpmYx3WcxQFIIhy4WUyeYdAvIF4GkmeSGUXMB5GnuEFQJw4ylHsCdGkfo7u4GgL+tqnvvrFN3is3nPA3BG/vRs/yQoCY4JNelyz/LAEm82D4UV4knLj0SVsRH/ZW8UbQFa9kXFZ6B7/9nba2LQBucDT+U8A3JO0nntZRq7z9Rjq340vC/ydp/O5H7pdXa9x5z7vadVxK//x+GsT74op+PRca/A10WpQ6o50J1hgXE5PlGBJtxAt5vQn5RkYfRZ9BgChC5SChdPLcFfscgGyrWESg7zb2vUQQvIeAHeyVx0tgEfptCJ3mAsXmT5CERBGAiBZaUNcnFBWgCYgS+jI76nlqAcUQcAlIBqLPL/xllgFE4BM2ayTZ1URVrIPoCMM51d2cjonbWGZpL0OvBsmvv3KVVYuEcpipVKuZa9BUugnHAX/mAsIzbnPfEtWE7H8NqEm5is4eC90MG/ifscTTMKrxEDpcKFDibYYHb5xCIcQFcoo+dibQ7iSZbG5n/5lM10UPxsBLZucdMt8iUNYKzU2+BrfT2Pb13neUYDGM/IsYKe8rr9pJF8bsyfscbmL5jYgV7uXAdpKpJ9btN6i8eghdMd4KaUnNA3GS9uZ8KXtyO/Pk5Joze1n2kuxNeDnSaeQEwMH6xaQ5hWctL/nNhbdJAa2QR8Q5rnntRM59zgagHg2/MfqbX75zCPzZPG5LR48EMDD381PBnfAgl2sAX48heQlnJSYc/LRLt5MljwBnljZUQZ59j3vec8VfIjPo3wBetrkfQe8gyD3ogCYxARckx8PI9fYbIZiw9BgTwVAmTqiBJIC5Plj0pzVRsg7k8scNkcsHQ+PfRYcHY1CYikqnhITvABweIrC8rxRkpUxv+0J4i2Qn/1D9DuGgOetCPOljbmORwW4ozjDy6yySrvzDJ5iaMm1PvCQNvcS7pZHpwbdxhk/22P7hA18b+t45W2cWmkpjJMSgd8rb+c3bp140vWs9lNnK5GlODodVxmthnSc143gejemG+BPd5Dj4KfrlLc0M6Da17quE132WHCFiT/zysMABfeaVyQLvq0U53d9KAvzDkVg67B3gJrnjiF7ZYctR9fd8WYLYr57VyyviWRnKvsGmOPIJytNeJYQiM/7T1iJzYDhaQAxCiVhl4BJgAalwkmc9KmPjwbIUAreDAWdj+RJJQ/hDeYAeNcCigHPAm+QHcosH/UBe4S2qLPHh40hwBo+onQCpKGDfQ6ElpLYNc3gdfgPj5Uwjb1n2keYCEAnsUcEpZjnWD6bsuK5sCkzKX1DKIgd39AX+sNXVlTR1g7/eBFGj+1ObbB5DDWQWaY7Zm6edfk828qhMcXGHmWv8PMyClcyRp7Avtv1LOV2ah64bc+anlIICFdSh4kcbzPzmrFO92lcu3XOf9KoJ+3eFr/B9lkZTMfZksqnj2NmMq/jkW6DBcaCYO3dgmZXu4WvafJZS4QO1r6Ftj6SPDgBDGL0jt8Tkwb4qS/5AGjaQ1w8CYs6lj1LS1mGGBBk7wEH2MVazqazL//yL3+idGgv7z9IHbbi2bTmcBnhFp9SijJiWSSHznEUA14bHlFoD+CnPBYT4EGwp4H+T2LuArD06i+samhgwQLKDu+BDX2s60+KUsI7Zb8F73NgHsMLEWhb+J3ryZfyfUSLvXjyw2cWBrBXgj0UvM40iTAbk+npNw6JZDz0OF/A7DHTY2N59fx1+T2eGOvGIHBpjX8rhKaxFdBJWd3d3Z5f7evd3lVGY+gaw31/5e/nFoau9NRZRivM4+tdoL97RQyELGA3UHa83MLSse5TTNCpXdMTgxfjWtkBggxshL832/gZ6G+vofnQk0dWEj2/4nbgMfg5ANPXu9/WhBrts4XFb3b2YoEDnvnunacBKHtV5MHS9V4C9gkQjgsAJ282PwW0HBLCMsVL8LJV4v5Y9sge3gTvRQZg4SUhpQAf4I23wyoZz83ghaQt7FIOzYTAvuqrvuqalwPqWDVG+M4ryHpDn99+x+7mXAvQYuGzl4FVWmlfPBfaaKXI3gKOp0Bx0bcoWxRMEivG8JjyPCe9MvFOm+BVvC08RY4V95HftKfHxC3Dqcehx4Xl/GT12yhdqcekgbnp9PVFi7+vMNh9bWhP4T6FsMpKWp79fWWTbvFq7kMw2BosegmjrVF3SjOcBtAI7670JG0TaxByPacG9j3AhNRzHavtKJ5mrge4E5ZJl8P3tpJs2bQCttCsTrUAdHy/FTH3uGbgz3VW68Bj94n7xf0OX/I8G8Gol9VAAS8s6gBDgCOgxt4DW/0BHwAMMGLClff7sp8gE5pJtsKxvm2IMN/CKijO3sn91B96AmqcdouC8vwNyzlZzoqiCa2eNOZcIO/KfV474QFIlKGXuyIbWPspN21mvsOT55SNPCdP8no1DnMWHH2Ra3h77Oymj/F26F/ysAkNrwWPNfMm8eh4uQ79ET6yvLTDlf2uYctvW+dtVBn8PU4ow+O58YZPyjeW3Rr3rstlOd96tvO28vF4TDKe3d2dJ5DdpvuiEcsA5bllyJ94kfRMy04NKq2VnX8xoK3c1SBf7+80ztZvd1x3jAUPptyitVMLLm1nwrAVUAv0stYNyH7G4Tm7vM0Pz98sRdK86UHHs/xx4BoD3MqFiUVo4jerhbCqCUmwLJJJZECaTUqZLCZcROybDW1JBh8mnomX5zvHVnOiqfsHfrMcl/kPW9SAZOhFafHmMqxolsaSlwlWJlMJvQQI2dTF60WZxEZB+miUJJQH/UvZ9COT3xwux3lHgG2ei+eQ9qPsUiersAiFJW+8HPgF0BuMPAZoEyvDWBzAzmTCZ1kB1n0ThRW6kzeLAzJHlFVXeG0oOY8Dj60ehw3Afd1yTDld7mmMc/9Ux6nexiGXtfCqUyulW230WPZ4dR3r+ZNCaWXzrG1/cCKswYpPA/p9DXRn+dOAxnUD2CqnJ3LbwmgN6U93WE+Iu363j84gFMLAwSozfQ5pOZTU7YAeLxekbsdh+fNrEm0ldHjJ3oX70J4LdHg/QfO9jQDKIEzjeLxPyGRSOYAaQOG8IHYcs7mKCWgs/NTjM7JQFqkTD4RVLBgF7icsXdqJ9UuYJteZOM09VvNkuSd9gVIIADM/Qd9QHiuIANiAIIfuccwJHgGfzO34TCkr4yQUZnjGLl/axot24E08ggBu7ic/MX7eGZHnvWkQxYQXBG0r9Mr7DVA2zEPkD6WYfR/IN8uG41GEJk565b0LnF5LOx+VF9BGjGnhWuczCPe9NencWNX3O90Hnj3mV7n9rMfYsyi8VjzOc1KM61qH4T+ZNCeVrdXXUq/+ntRWcgOMFYwJXpa467MgM0i9mqMFxQx9FsGyJ2FaAFev7mFweBlddw51PagD1ZKg28rNIRtP2ntpr/nZ8wtcc5kWkO4/LHraaBp8/lKABjrgCTtU2Xz1P9u7s51Ls6tq0BnhrLoJGicYg+AArqmulStAIDiycSPEcZ2WwVk1Qn6ihkfMd39f2tKvXyKWtLX3fpvVrzHbNReOWthp3C8JSTTSAFcAjB+7+tm05SD3XOe7jiA42xgBUB8cedrAmIzrptYDsICRh1SeTRsCeKlvOG+hvJN4/lAj2fCWRN9OjWTXsX5GOKmV2oBr7KhwGILt1s67wJTnUdqa+oQgMHQvwc+4UHN5N+qcEGP2mF2nvc8kbUkdBBPkFMATKnWMtGBPRcqwezl58eKKajCJUbvHulWnHz/eIRh67jZm9PePZoPdBZAXIVhwvAjR9b+fXYbuKnPffarbEpVlYvv3Mp5XPk/fT+U+EYuXG9PWONyGjCtdBKIbhyvsiXJx20kme7vJ7TPe6wmwBKtVLUlr8Np6fnN0mnyBZnve6A/5d1A67eZfDnCuNiEi3R+Ikfq34c5nN8u9spPsxFanDYXR/UT0zwKnU2/QBXwmK/BL6h3MIRodvXM5cLuGxTgK8CAK2o771wbtCdDjbPWZPQVJyTvglu+oQgKcvIcCXiEIaavxQZgAOklm25/8064kG8+0gauqcnhy2UgnXLczjVNedi5TZeHq026EK/9Tf212LQQrZQHv5GVXtL4zN4wZydc8cL1Dt+T59BsjPrsF9VdURSnPrm1eTnnWzuxePz1/d55aq8uBL1P5aj7v9U5rl+hnlwg8lbX12Lr2O68Iy5UaVzvfvt8fz11SCKzetPh8pcfw11eHrM58K+X/5rX5dGUvYOyOwc10o5ugAAad0ODci/zqtC63Aabb0Xr39sBxnzueerVOevPEOeqbBvL2BmkVVLe529391a6Tl6Ftiak+bW67JY+uWwM9f3QeRLhquuzm5uiYhXwOWGibU+psriJttCor31xWEZl8C7fAHbKNo/ImffBSYqNwsEyupU72AeS/EM7sIr34BX7LfwQBGLMrGXOSi3J4RvX+jbwX0NQGdQgBzbvsE1Ft5dlw4LlPvURayreQ4jyIki81m/vbr5L/iL65ZH6QyP7P3+1IzseY8nJyChy7SupHEhTCQ75J1og10Gt4VZ4tMV9Ysdd7vr/izPuZZv6aSWwM6DXY969nr98Xw7r5a0e3dxnky/ZyEZElVF2Pi9h0+sLLqEHoiYpchfezPela/bSc7Fb6KmcHye+d2A1qa4BdArYd0/ntc20kbaIA3FaF0/n0JP/48cvzHvo51y7X3QYooN+ATVfdE5dqgtQhH9eBVY932zGa+ABUINGbwZLa2Aj0AmbAMydvAR3urNQwuR8OHUcNXPpUtlbJyNN1YwcE297Saj/ula7lGTYA6j36fSD/X3VuAnVZCInNaohe8qaeYVTNh/pFnXs/Qe5FB484GiNeWnkPkWWnSD58/0lAbVviZptykjd7wjIi5gxPIAZ4RL/zpCZNXfIJ0bK7OsZmAfFIMsY59SSRdbnyREy01dx8AqvlhJehs2Z7LV14coHlE0P7lJ6wcTUX/S1d2NVlY+y2Ps04bnu6Lle5b+Fsp3MfAsKw3PY3R6EXpe3UnFMTgwX6VwPS15TXxtsWf5fDaOBqgrfUet9VRtd1JxMg0aau03pFbd2bY2rCghPu/vs0UBO6oNvWcWnyn9gP5JsgSIzkTQwsVvkgBPTbuFyb9EgJeS7qA2oKnGTuJURFwElohCQGVOGeecwANOCW57iNBoACljaVCa+MW9bHbaQmqQA/dQXoyceGMGDLZtEnozlJL22huhK7Kdd5LiFmdgUjMKKJ8mBCnNIviJ76OiM6CYHDqSPS4cBtknPmM4eHlGOsSYHGurn1Vk9iMrzXKr2WJFMPBwlFTZSQI/YrhAGI2kjY+NRRZFrtI3m02s8auNZ9r9FVX/f9J47e71dr3NrS5u6XV+kiCq+I0DKa+0zbG3uHeed7MbeXymjLeE97pG/femDVHl2ZFedcX5fLBsXOoxv3FuG5yu7nmnhteZ3e4gCeBlmewMXiYWz85psvxbedDCvytZG9J/5yTe673j7uLVG0eMn3veP9S1QISa1u6nrJ3zNUELjNXjwBJW6luSdEdO4DUf/Vx8lquMSOUqqf24Mnz7Q6R3jypJYakgBjwCnEKfcd4xlioP8CvMA0dU4S7gIII2K8ixAl4977KtJOrrw4fFJQ7gl94l3SjHhLbBoOnrEjvHX8SQ4jYtxPn7U7dPJJO21gY8fRb9pjvM1nBIz0ZF5oo/ztQ/irv/qrT1JC+jaEgpNBnrNZjTeUcWtJWNvam8qa68+1npcwNCPbqdff5tvvLHHZ9OregvR7pZx+vu06GBy2J2N1EZeWbrquF2Z2fZ/q+HIfwmZ4dciCIG6j1TV9fwdaHs3R9/ObeoIssPYzT216otDXewvsFkxz9CtRXHq+lV7kdak3emFuO7d9nm/9b6u0uszWzzbRbrWRdztyqc1UXDn/jzpKk34/BlUA4DjGAEjAM1ywSR5ADPFIPlw58y4vF30FsIEPNVUT0O6vBhtEJ+UIwRB1Tu4FuMThCaBSdST/PJ98QkAAmR3C6YPo8aPawm2nPMZ4BCkp5aedVGIkDl48PK7YD5Jw54BZ/+g3Lp2AH7FixNX3yYfdRV+1/U29cf47j3su9qFC2tWq0/RNVIEdaJGkxS2XgTl5pO87tUTSdoWe4w20m/p6q1HUedUuF1PW0lDX6/IiutITw3v9vohGq+aambXGzJFt3wL+MtFLcH5IerQhNOe+4tpFvRtoTJqkNvr2f/l1pRvYm0PtQWxO963OXn2+9mxZ7je1bS6iwbV3jNKrm1x9qIy+RCA7XHHn1wC4Hhhdv6Q2Xra9pOtKVaEPAHl/mmgbD94xLY0Q7x3G3jpgweqy8AFQrmlrEjUK4iYiZoCsudHkQ0WjjnYGO9tYjCKqELpx3JM+5RrpvZQZIgVEW43kN2LhUB52BX0HHBGaELkmxgy8rSJi/AbsvHO0saUKUpPx4ylEIgv3zWiLkCalf9JvIXLWXd9vzh/BaZWj9YCwm2vUTOw6PV/Vyw5vQQpDLBGcPgMh74pi227jJJvVQDSgtW3Od6uym0mwpi8podf4csq7/q93t27XtV5bK21czPHmo97GsW1E1nePw8Vod52eyul3rvQY7fSyqG+BGut519tmsAaUrqzBvSjsNXCdX3fUe0SoHrQnCWHLc68NuA3ejIbNaVGjtB2g+6rBq1Upq0dtzq7fAy7ES/8BtfYxxHpPX1vsO0kb7LlNimmv7bjK5OGUNEZX1+QVoMh9fQLQ8gxgNMlDSHJ9xxyRSArAUuvkmv0APf75HyOmXb7JM0AfSQHxyreNYCkvZfOj5/Vko5aopCQlHjXCXvDb77lFgmRzMdY4TzaE/Nd3fQoeicg78kRcG3CsHec5kAKAGsmuOWHuvcokdTQgkTR7ntqMaL723E0fpg76FNPmUJ9IaLExZHMbb6hex5ea5/r9JC3s/QX6vt8YslLJlveetJoO15oorA21y1rmOt/Gct2EYYF3X6Unpvut9O2VUYtzrxq6HaLyvW2/udClbL6buvaA9TP73YYg11otcolZ6tv/u139bD/XXj5bF+9uH7XI2h4UXW4Tg6T2vGovH2MiTwu4D47H0euXJhoXh9K2gE5sAwC6uTicOp13jIh88QEb0ItPfa4jBkCaCiZl98EsCGnrTeXVxJAYzTDbm/5cw7m2DYWx1zGZASdtZBhlPGfk5uYZIrNnUuCel6PtxJc/13lN2SNArUKiaFfRlJf8bHojKTRBQBQxJIDcGOT93LcXQz9amwC+183O25YKm0FEGHxCEP7u7/7uMzH6j//4j88RXNUjY+zcheXIe+yXGFzMWwPwSv8X5+27saGJ6ub/Ki329TXrTWqbwT7fqXGv16T+udTQVxtfXb+I0ZXfaUNY8PR7gXMLv6SLJRhdmWsA3V9pYtVWq7//+PHjy8F8a1B2clyTx/ueo1ZoLxFcVi+srUMDLILQ3O5OXqC+3FR7jbS43FzaTrb8x7m1Ogcwyc8mMKClf30z+Oa36J/KDNCKl88bpjf0qSs30N5hzEulbQftltlunXYo5zfQd94CXfaWJSyDyLUOryEFBChzj9QiPARQpha0CczY7DjayGaONleNQ3detP4H2iQ0kl+rEhiK20ic9vAGQriSSBohMPYjtErJnPQu54H2uGkbAjVRkrMe0kfOpf7xj3/8We2ZkBb6LYQgRuhVcbbUegH7qpOkBtpeK9LTe09MYOf3FkHYfBqDuv7N5G5qDGri1mPdqvpVf3eb30MMrv9P104JwQD1C63e6WeXK3K9C3zq6H73MgxfddFpbWDscnTo5tV1uzpSnp13L8R2xWuXPL7ePCw6UmRLUwbV/5aCfFpSUAad7aoKlMHve9vY/dbv7oedYeP2Jzk9zMlmygC4gBsw4gTFLpIHzlh/5T7ihCvWn1QKvSMYV61NeSYgk9QAhtDJk4spI3fyA/C9ZyLSTOoVXfduPGOczn/G2wajVXleXC/X0Q5lQXXWEoC5aizMBWPaIS/aXkE1Z4xx4Z5xKFCH4mBrYitorrTbpTxqQUQv92zmy7OIb/opnmVOhnNOdNtAeL11WJiWDp5AeQH9wqMr7Xj1tWYkn8D1VWosadDu+z1HWvWz+Nn167Y1M7flXQTxqQ/eQ+xOCaG5URVf17CuRFM576tEG0hXBdXA1ODZbmrdUZ1v/+/yW6TeATLxWvRv0AdKqxqwKBq4LHQg5+hH95YLWI6mdbTsDV1f3H9PJO/pPwbLllh6klEb6Qv16jHVj/0c/TyDI6mCH33uhYP+TR2mDuSiX7dRLe8J55D+sRcBqOAO2xjZYJbvdn2k0glXz3U13HH+6wvEOQTDfoW8H6OnYG3ODk5yzkHK506KqOTZ9vwJ6DGQUscIOw3U26W35w11nra3l1YvdASawdR8IrW5Zi7nHSfQsU14N/cANUmKNOE5v/PJ+DVhQDh7jTVGBOSFzWipNGWkn1Ju7qd+2begruaXstqg3aBpjvRa6WSuN3ZcnHnnu8zgEpb3Aqe0WNbMaWOAa4uNK1XIsxnbfiapNTD6p393XzX2bH9c6dtXjcTlLCVdIF8q1VS4KVunfXY70beJ0+V0By4HfFFc79IzN6fPeNoA7+PaSgneV0a/0+V1/yBwrdPX/iUeOFeArAxltrcSMO5+xVE0x9ERQNfdFVD1ZEIQevcv8CaVAKkYDQM4VEXqrGwb1pJw6tRs1GY9tjhkIEtNxKc9IIOLd8qXPnWmsoNsQqBCMMKtpl45nS3P5n44a/2MGOwCF6HV/gVz0X/SlN3Va8S1jqhzeu4nUcH0nOp+aY68nRu0t9Vw6Z982++Q+x1Wm3977vFgaSMxe0uIJGJr7iAgGBXEpu1ZIb4JcPeLX/zi816UX//61583NDK2NzHrcV/Qaq7+4r6v/9LO/8WGLcczP4QgXATglZTiuxnZi3D43fm3FHVpU7S5/3c+y7z3tU6nl9ECfHNsm9kW3nlIq+93ralzV3QpaX/6mS63B6O5DIBPlSO+CgDf+4hEcymrKuqymgBsPbvvto1PNo8meHTNuEL59iTv7y2v90z0ITDE9x4HXC6QQQSoBBhgqSkQq1wPgLAjAHOBzRifO1icsnDhPcHtWN6F5l1hmZXfYaZTV/aBvENNlTJyKluIQ+qYe/kdVRhOF9Gm11fHPgeCQZrOPXOJOqnXzToEIG7UZ8YUMek1QpJDkJvoKKNdYuXbHkG5zh2YZCCctryAf7uV5iCcPPeTn/zk85jqF/Os1TzsEZis9EX6N+djOzTIBsFmOnoNNBf8BGS7TnZ9Xelah533/n4q673phxCR61pjYZJxXGZ623UB/VNq7L0krqTThuDl1qc/dVZT3SYam1f/7/vdAVvxznOJ1OZDEgHovsWN4bni86QKWhG++2HbttebM++B63xWnHvqH8C+arGkHheAapBxfOrfnD9VREA8gKAfus/YBSxgbooIAnWK8Mg4fvpjp4qxBWQna/6LMApsk0+e7x22+qdBB0Dnejj9eAcZ6/xOPYVhFuoiz6esqC36GMzvS2LkIou4pP3JP/9DLJLUgxsgI23ysMtZCA57OJqbXztOE4oeZ+PUzJB3+sAZ9hRjb7yohOjmqX6o+qjJzOvu82YYhMIIsItfZLxW5died8k/BOTnP//5J+Kffk8e6c8Qgz5YqRnADmHRDGD3wwWGT6DY6Yl5fCs1s/ee9N78P46tU3tbRddMxBOBugjYxahvv/X3/u707VVYv9Sg9aqjVjpokHa/B7JVLk/lS60n7LrIE9dGneDwchzcSgVAnzpo9Xq//e2XBwJd7ez/QECbdx+B32tsvjij5pjVoXX8F3GyMNtO0/2zOvs+jUvf5b+drvYj8M6hDhHvP3nxjAFWNrBRL0RN41xg4E9vbcftupsmuZ9rrf4wVgHsqHxE1+xYOQzIqQv1EOAhLZhTIVZ5N/YQ3C/CybWWTcP/3E+5Np4BcyBrrFrds+qjvub5vveUeg0Y7w7XQWKyaa83f5EEgLu6kpozjiGsGArhMwAVQt2eTBiP5B8voqiKYisgManbnkyHaC7z+bTu95leD0+c7q6TJ2bSvT80LdheGPrqv3dWnaW+mIpmAqXm9LfcS63UBPZdBEHGVyfuIDS4e6ZBaxu6zzdYXlzTk4qqOybfYswwcFGHMIIS0wFf13OJk9+t33yLKF75NCe1Uka/5z/i0dJSg/VyDTxDTBh6aAvU/oQ12gnKlmeygPNpDyN9DTiTbxt5A4JZ7OlvOn1EwD2nqNFnKwcwA09t5DGjvh2Yr3e3Um/EIyj/U4cQBtE3k1eIQ+ogrpKNZuFaGaUZmencm4giNkJWJ9n30GI9z6tcs/+ivb2WSUCImwHAmCCQPd8BqTGQB2+fPEt6YdRGMIEtA7S5oO9d7+CG6ef0T8ZJqAnMld3heacdDHq+JQ8hQiJhxW6g7xE549F7Z3r9N+Mj9TroNYBBaObHNb8/js2z16lrmy5OetNil+cbw55wosttSXLbuJqWvq5f+h5i28ZmZV3Y3Fi+6SQI/VF4T/Y12KjEAldzu1v4NWBXXq2W6UkPLDMBIwVkkSIM1EINis21t4tfcytNBNSr+6XrdXVm1/Vqz04UfWhxE/l7D8F6Iiyx9d0Ly7PdPpw49Q3QZFOwgPUPbg4I9fuICD1znrPximdK3nc+r7YEePp0NJIJDr712R1pNWMZ9YOziDPeyV/gOmClvuFScbgisyKUkVrkE1BPXogO1ZK65VpUKPz483zqTv2VueeAn5Xs5GGhdv8hdlQ1pLWWMs3bZibaFqWfjA2pOPc6DhI7CvUS+wxpx25zKi4eVyTtPs8hicqw16KUvLi45hmqNv0h0F6/cwF1M1hNVJupeovT7bQMW+POe9IyhdJF2J6IylN9r+cbW3uddz77zt5f4rSM+arjpMcDcpayNNi7v+IMveKl0tjGNpD1tSY4K17mGiMwKYDOk9GsvYbWbaupc3Pu2/G7uNW7fzdh2P7ZSdP91pOz29YL5SJO+sTA9max3pMAVD1LRQDEhZywgxh3CJSBMNfM5CPOUW8gs4sXQevzfIGI+vNGSdm4Y1wNYsQ/XYr3kDEB2HnvV7/61SeATj7CQJMQMgeyISr1DnhToeQ+qYauP4nRlRMBYoxrzryimqKb7/hADejmfNukGpxbauRyusxTUrvgIuS9vsyFXYekhvbeQiR7HSL4rd6zl0MfmJ/NfCCo8jQXhDdJvunLjIkQ3LkuvhUpkOdS7sML86EZn8WKC9TUcxmtJ8B/Ui9tWuzqNb2/+7l+d+8ts9B5bd6LtUsgPbfEwrP7bmPIqqY2Pe5DaPBsw8c2oBunggt2XfkWDbdDrsboyPa2CREQUz/fwhJ03sq/QLUBae0TS9Au7qDr+J7Uz1tozf00YOAqu6+7b1tdt8QHUQBo8mMMFYk0zwmzrH+BCqKLOyTyC0+dMhEMHixA0BnAVFq8XxCpvJNnsyfA/55n+phxOIQredqEFrDP2b7OH2BQDYFA2EKksmO2iY8TyGyuai8n0U0/LYbaDJb7DKO57yQyISbyDFBnZ0HQvGszWruG9hqjfumFaozby0j/qvc6DriGk9bvHUCvy0veLbVwuEjym5quiWdvYEOgGPGNsSiymV9RIWX88ruN9osjKxn2nO853i7RvQb0eROwZdiWoEjX7wvUN+nz672n5+W5AN7X+//HsYNc+XdfLiG70sW0dvqCILT3wDZ2O68HoJ+5Oh/49bMtUWxZ3VgAn8kScAgAAAHEoBfc1YEXx736y1dtvdI1cZ4Ipvqt/aXr3Z4mCAc983JJXedeQPqqvYNsBuvzfRuAGYapA0gAuNuWHlIXBmbcJTVOFr+QFfT/bA3cN7k7IkxNIAFagANgdb/mOnVEgCfXRPp0bnOIQQApdgPgQz1id3ADBnBh1+g9HerK1pK6JQ9MCS47z6Xdfc6D8xfUvTeBNTNEzdVzyFi0FOI9/dUMVxsWkyeJaQHCfW1sQNP/Hd8I55/vjG1S+tIZESRyhnntS79Yn7lnbpBWESL7SdiitLE3+jWoLzP5pGuXLpX2tX739wXYT2Vs/gvi/c5KLhcxWB3/MoQL5vqi69A4sZKK+X61LemL8Nc70ZqDbvWKyl/X5dG60K7UVlTZKxKSCDKx+DP3pz2G+r0dCB3XHdHA289f7+7vV5NCWdq4HMFy5BY5w3ATg6Tm7nB2zQ32IklCHHkJ4WyzYBnZV0oBQO015DfJoAkZTjhAKHx0ny2NCwXowDYg6Z3k2cScFBMgz7N5xiazjLEzku19oJrQP7meNkZyiA0hqiVtwKXbOKfv8p5wF5L5GkALkOVe3ChTBwfkNHPTc7bXhT4EwE287SHwfBNoxEt/kxj33ApzyRi29xRpxfpTt55jJBEE3xi1mhbTJl/zGwGz/0BZ5rS6Rt2WcRMvCuB3fwGpBa4mXq/URp3HjmGvyf19re1OF1guqK9auUG7VYSS539b9qALuK96r22w56C+aEN1l9k41ATjavd5hGbr5RTQHOXHj79/2Eq/s9RwO3M5472WRP2Du2DYAwrNRWweXZ48u0MbcJ84+76239ek6v87STa/Bo5eIPtcT5aeXNvv8um6UfUApRCGgGU2HiVxH9w86Z1FHwU07APyaoLPvTd5eY+LL7dFYMqoKS4Q2wXdPTdPAdwcyZh37QxOnkJjiLwq0BwuFfeOm0/dSDwNjurkKEv2hsy19JU4PCnH6WzWQUdhbbdKc4DxXL9ZvE0kO8REE41VdyKuPWf7vZ435naDQkvmzQiYs+qC0Dcx8UE4MGjyaTdkhmQeR+mX9FtLNPJaQFO2umGAek/KcsNSM2CbrmtvpQZRZTWwd95rG1CvLreJaa/Tvo6Qr91zy3t1b5kU5V3vXAQv6QuVUU9qE9FEU9l291oK153UhGXFmQtsPQ8YstAzoaIWyMdGquvdbqgBvMDyeufqqOUctmNXD3o906KtZFG06JZrrRrCrTeHh/PqnaEmER01cbz73vm2jKTt5tjcFcMw1RI1hTzS/2Lm4Bx5rCAwuPzk2YfQ5N2Ab7+fawCeOkZ45LyX39QXVDWAtc87TjuczpbrCb2c/ALIwjakHTySnIyW3+IdJW/eODkbIRytg3XYGHDj9kioM0IijpUNfMJs9PGHDcwNbu2SmtSRTJXL1RNXr58XtIxnc5R9zdj1/G23V27MDgkC1FSDvffB+zzUeu0nv4xJbD7e6fWw4VOse3XotdWqZR992G2/1v8Tp3+t9+t+M2DXO417S7R6HVqr/d7iRf9fddjWf5ngJ65/iUhj8ZVeup22Qbkb2lzIBYhXh12UsZ8DSvzYQwiyMOmN23tjG9NG777fKpUrLehf9333YPb3U57NrbWhWB/73oli0XUe/SxQMh44NwspSeA1+eX5AGUAsNUVAMdubuXkHf767YPe5+pSMeAmA4TJw6H1AeeOl9NB5Bi6myvKOEfVE8IRMM+7+R1uPUQk78eonPx4SxkXm8YCPmlj9iowgnK9JNmIuCnUhYNzjEmkkrQfQQkxRaARAACYRE+uvjbFtYHZOiJtmZs9d9tmEsLdnDtAJHkZV/3XQGCesAckbTyq9hBqaU89/Gfwpprt+Sb/ZRqFLfnlL3/5Wf3VGKI91s+qkTynLG1a4O31tkzlcvMX8D4B5cVJN9A/cedL6Jv57fuYrPV4etIsbJ2928R3cbbfa6zte09E4TF0hd+rPmqupnXbC77XwK1uXUfIw6LKos7iFxu/uXwTcFVA/b2dc6XVrW2nX89vH+1zO3Gaa1ji0hNX23aSbr+TkHqikQrahY9fuXGJV48zfgG+3bj5zy2UcS8JRwdcAsgdY6hVQwDCHoS8J/S04HApxx6R5CGEduqU5wIgpAgcd+ZB7geUw7nn/e++++7zHorWpwbIQzxyzq/2u0elk/8IRMqiVmrjNeOzcNH6Iu1K+7WNEdT4mr8hanF51Z/N7e7xlbt4Ed+k9gbqmE+tul3dunKAvvAhTbyXW22pkss2F+Xkkz7XB61GcqRp5516ZGxSbsbCPg0qpiTvX/O/7WJN5BbMmrFasLvSxTXvmut0MbIXLvT1/TzVoRm9C0cWrBePr2cvYnCpvZ6wudOpMupOaErju6l3V04Fmyo2cK+xqr0uMuFCADKRIh04f7dFROX3tavzlXVxBl3fpKXMTx11TZJ9fidrG+RWr9eLu4FtdYiXHQGHl9T7DnCPykMUBG9rSUJZVAQ8cPjpZzEDI4laqw+ukQe1kXavMZDKCpjyVAnwOXkt1/n059nUNeEnSI7h2kMceEvJm4tr9NYdo6jBPnXtw2G4ZjZg0v3boU3VoU2AuYPSUeFpP2kLaPbi5A3VET9xzcYt39RZNtHlOu8vEh+pzXpq4tRrtyVU7cUAmFsdKkQ4EPlwOTYne16aX8ojkfHkspmQhLFrtzHjaQ1iPM3xlpDd3zXZXPqVmjl7RUjUpYG/625d9rObf7fR9WUI9EvXr99rBrAx9WJol4B1Pk2EnojCGe3UZOhJ2gX24KxKZkWkq3ObCLRUEIIQbpbL4hOg9yTcxnX9F1R3oPraNRhJ3fnLAWjrvtN13u+eVFtmny8AXFvFY2E0AfG+UB2kBQY/Y9T7CRh/JecYeBcAIzZ535jQt+eafQfUPwAwBCh52JmamEG5jriIXEpiCHC4joPM/Ugl+eT9gFQOXqFSszERSOfMXqogO9hTH2f9qk/aHtVSmI4OUaGtVGUNtq4jWKvbZyNhz2HoJQ0Z/1bxNAGn/umxSlm4c2o9NhdzBaFCDJpYkIza6IyQcvek9qFKQtxw6NrT9iHEyRrON2krKQEHM1Y//elPP0l9aZ/d4hcomb/9vUypuqykfTFovZ4vpq3vdRnXWu21vNw3bOxnPny4I7YaQ2kxdZnbzbvrfPXN4p3xvFITi4sYnjuVF/T8XzVNq3K6Mhd4dr5c1XgRZXGGS7RzVmNM5h2MHrieZFvn7oTtlJ5c23k72V51YPfJJQkw/nYfqOO1OOTXuuHVFXebcSg9Fvm0iyHQyPtAsYk7sEuiBpInNQNVlHFn9M898W9wyb2vgZcQ9RCOF0FpA7kxzzxwqDzQbw45dQr3zHaR74AQYoCDp7ZiN+iAeakTQkbXrn8RGyCL66U/b1WaiLqAlb5eWwAvoDfOJA15tQ3Ns6K42tfBzmOvAwO8erejQQOFPMz1VmX1c9qq3giQEOjfVlyidoxIH6sXQpwyojoLgTAXEcCew13Hi+tWxqqlm1teBmvXrf/SctbvSV3mErK+vgTmIkpb31Xv9LPdN/m0PbKJU7/bWoquZ/fJU9vPjWkL9l0x4EB0W9DXWBN8iQgdLMkg3Fs4rEyqza8J0QXQylywfsURbCf176eBuPJ/KuPV4PZgNLi7t5z/K0Ig6dNWP+Ew6ZC9m3vcKAOKPIWAS7v74kCBD6kF8DP840rpmwMIDWCpV7ujtsoxZfFmIXU44D6qImrEgH3eyX8xhFJm1Dv5H+LQ9gdqqZRFXZbnuJSSlBAv7QPOyvVeorWSGJZzW703rp6OvdtFTWOjHmBt6UAZJMM2KNuNzZDedjzP9pzrudvhIRCzJMTLHo8G3WYCrHdqQ/VGEHl+2b0eWwoilDJbWlWXZRovhquZqq7bSv3d7qdrzYxJyzxeabGg+3Uxo8vcpKxWd/ZYvap/14MWoZ95wqeL6XyVHmMZ9aQwEIAHqAP9nXT9bIusJkPedYoWQtBc/qqhupO2Q117Aujr/ScqumC7hElaSaCffTVpetG61/rDpfLXZMa5PU2oJrz2CODOqYuADWDJ74wHUEhST7p93CwOMpxfxs/5B8adDp23DrVPkrFHZJrTzvuZCwHjcMY4eXaAPhM4z0d9lGQPhCB6eVa4Cec+KyfEIM4K1EwIZupn/wJjqn6gJjLPU3+b8Vo3jgDJszcBps4hnvZ4tOvoSqptkzDPEOhvag0BlyUcrVuXX++E7mcBbTtpsJu01KL/Mx6AvSUL88y+E20OUbD+SU7tMbjzen933+waeWLirvXY62Pz2LUtLSPXz1/X1bGxZYnIVf4yhtKT2sdzxq4dEbacxq9mNp8Y2qQvdioTSy3ePlHsugYIcDFNQNr3uQe5uZ2OQXQNSAPdEzdx2TmeBn3zW51cc/hb7hp2np4xYNvuBe2eOH1/CbBF3BwGXTpbQL9n4QERuvI+jJ3eWtm49N6V3Ny/haxtzgTozWBJPEwAOODVDoAS8BdRM/+FiVA+tY52i5+UevWuXaAbQsQl1KE1ytbOELEAs93G5jTikzws6PSVndAkHcHd6MMBJ4Kaa05ks2FL3/U51bkmryb8zenrb+WrW/K2h8NGu54H5lPPkSYSXR5i0ExK73huW1TGynjIj3rLePHOSruyuzvlOK9CX3dIE0TJWrjWbf9f8Hti+Bo/Fiv2/0UMrjyMT6t0jYe+tn57nerHi4u/6rLc/RLJixHtOl35dJn72fQFQbAhSDx0g2hCIwrAqglC6xV1SFdYBVpyUDHvNnf8BOI9+H3PtW7PvnMZazy7XhPKbhfGHqxuU5et7dz/LOwu52rfVacmHOqxUthyJfLtsA3USAzPnmvjMe8hrpmAC4eYZEEjBk5fozbQLzFSd8RUqqOUlWdx9Izh9O95Bhh2uGoqFgwFbj55O0M5ZdqkJjJp8sp/oE8VFKnA82xZ7AqfFsbvgBTBYizuudoSDrsGgEUQ5NNuukJuh+iQvlrlh1jpN+ulQQQQN1A2t7/ctXySeiMZjn7VKM3ZIhTWhznHA8u86v7J3MhRnHkvY2UzW9df3RZoey31umgtQuPBBa5vpYuAXGv5STro1IxiM3xJbDZCtVBhtl1xMeyHtKHf7+tPROGt9HsEIZmHi+pDZhgW/dZQz7c75DURVTCpO6tVSdJySttJSwiWu79SU+uuUy+Ai+vfej1xFUt0lHOBfj+3VPqa1BaX6xZuu0kCaqoDZbMPABsAn2famIkABFTlZxIDG3r55INrDueXe45J5ErZp2VRrQQoubwGuKltSBe4UEDIpTXPI06ADhDlfaFMuHIiAozX9l6EyOBuBaFLPvYz5DcOOH1Gd27uL1NgwxXJIN9xh/3t72wiJK0m2h2wLs9knTloR9uNUZKw3EmMwNqAyWgJ3DxprzTJM/LrzY8twVsXPe/0Ae8mDCOiwimBowFib56SMO17ac3CroXGlk377BKFXkObem1tv2wZ15qUntTELd00RllfIfyxQ2XuW69/SLoIlvKf8GXb9oRj0u8RhAySYwVJCc3pX2qQrkAPzlYuqZ/v9zV2B+BpgJaLuBq5+fVgXpNwB3O5sZ10215lNMduEeLgloh5d/crtI3CYlLX1RVvv3Sfkw4AXQet6/5wFoGQ16TATxPk2///cPR8eAwxLgfscfN5J6oCBlwgGw6RKiW2hfwPMSCpAFvlAeDkE8LTHK0+ySfxhsJt57kAFsLmNDD2j9QboYlUkLpGQjCnhekAaq0HJ5EAe8QUiAuzkvwC7mmbuZX3uv+MB0BMXyBSvI6oixq08y1iLckTQQfeC5TmgnKpt5pp89yunVYlek64jvzHzBm3tD3jIOxH3osdKO3IDvlej6QNfapc5SFEvf4uwP3wIClcBOJitjz3pDHo569nMS/atYbixpBW4XZ7rrL22nW967Bqou7rffbCt01fEITsAzBQTcV3kuynO6EHwKK7qP8rArCD6n5f28lx5d+A34Rp8wVMJiUAXPDeiWFhtm6+y+9JSr3Q/dkf+eACF+ibMDVgX2MAdOj7xdgBPk4aa7VSOFbSAE6avr49TqgTce2cBOi2wxEFEHIveWqLReFMgYA36cEJZ7jfADfpgGHXXEp/AB9qno7Hj5sFnsKh4Gy1mTqoD33RdsBtk1ry0z95J21NG+20JrXkORvwqJpw+20PSZjunEEcSSGEhO0jdUhdEUTzCLALs8FJwPiovzmCYLcHV6tpzKteP6vG9Rsh+E2FmVemdglxbT7mnRCKf/qnf/pUvv0dQLRVJstodp2aQOwzT5hwaQH6mX137/d1/XmB+AW6mzhLtB2pPdOU12Vf+e59fQjjGocugiCft4jMFzaEjsFCXNawlQy2APeWGvXvLqsrev3u/LaeT41dYpR0TZCtUxOPpboGcOv/igjKs0H9mlT9XNdXfQBb9wduDSeP8+sF7XkqFZysckgJAaOAaK5Tc3DhlCLqAry8Ey44YB7gxGF3/B7hrQOaDU4MvFQ7uW+TlPN7k4SrWEMs9VKISCRZBMheBMZgrpD6ADHAqSF6CEXAqqUmMf/zjBO/qD6sA3GhWjVHkmtja+uSEeokNpj0ZZiwuNnaONahMoynubMqGPMEA7AhK3rO7NrctQXMpfWEotLKf4cOUe+xi4RIpq7UZqS1vJM+FcBw8cRYaW8zOavWak78WktN6Bbkl7FqhrHz2LXp3c6/mTqM4BKP1q74mAudZ9fxqke3reeDMWyMutrSfftKKvrC7bS5+QX9Bb1VtwCufmaNnX19QbXL7Hz97ryv/1fHdplXB3W6yrom3RKzi+puW9+bvGMibT3y3ZzgEmjPAA0cJTBkCHSYTdsW+PXbzEUP7zewc/iLWEVRE/F4EVrCWQa8h4ACFYnDcoAsANtzjSXELwCaQ9yjbknyPECm/kH8EMsf1aYynkxUXXYx9xnASSQ15wlTMzGIxxYSAoQQIDQN5H3WwfZl+op9o4372r5gk6SOUaUJ181p4UNJwC0xrDeRa0kXB97qKAwJOwjJilcWexYCoZ/oyu3FCCEIEcy3Niw4L970vE9qAG5ueJ+7rmn3tV6u9xfzWj20H0nerdPvMleb0ID+Kn1/SAXquerzTk9Yt23v9IWE0B2xk+IqdCt8DfQrgO3JcFX04upf/X6V1ytqvPXYOix34tn2omkC1nn1RN+0nAs7xIrvvaiba7rKQqiBpEmIS+8op1FZhEPN/ahpwm0LUCYvgeiSqA16b4K2eSbce/LEgfdOYSAlnETKCmHBoeMuqSKAkMi3kVaiLkqe6puUOnoPIdRn+s/JX4iXdjGABphx0zh0/ZVnGJPFVRLRM1IWV+q834fQG6O007kexjV5iQLbLr5Uc+IvmW9N5BHcDna4u7T1YVJLvz2HqfzU1zPGqvM23+3LMP68qlLfHEyUMW8VJRdzu887xIi+MO+XY39rjvfa9N4SmF5Hi1OrNuuyVs3dXP+C87XOfevHtkk92TQvoF7gXwLx29/eO7X/kPQYuuKtgenfOvb7B058K/cEjpv/9d7+voD8AuKu44cXkkeX2dR7B0seFouF0YO1z3a5nXdLBf0MVQmg7MBqu6BaN4mbS8Jdtq48v7M4k0gDdNfE/j5cBvdMDSCUtJg2QDj5Or+CRwW1gTbyNAkhoqpJ/Rmq2Tu4lCKAAfJsdGoXxnZ9zW9j0YZZxmXcrLHJ/wYoqp/Uu+M2MTLnP6BL+4UD551lfPVxB4TrvspzNgA6oCfcfu+NEBNKGJfWy9uol/s8nDpCbe9L4X3VMY6WoTDv+rsJd8/XjnOkjvo1hI2h3wFFGJKMdZ5lQ2pV2hPjaF084Ugzne2U0GtdO3f9XgxVl7+ajy2jy7mITlJ7X3a+Lb1xjnjFNMqjHR0k77+Fp5suoiN9+80b6aKSXfk2QLvfBS9Qu74VvDrk6b0nXfxFELaM1S++NRmX8HSbLgJ46fIuu0p/d/+1e2y31zeAEAOoF3gTFEDNGGsnaa6H004wuCxa6oqkqGEYVQM8Yg0lKQ/4ZuHj3gOUDJ79PIMvLlV7AhghRFG3tCoC50StREx3LoKwEzj21CNAxJ/frmVguJyn8pMvMG3jLJ/5fPKecxmo2pQdABf6o8M4AFGgbrFT16kb+4VxsYcDMWFEb1ABLO3hhWA0wHZd2i2Vkbv7xJggOObZApW5ihD1buwQUIb6jIfzp9uAjtkQzv5aH71Or99vrcFl7Jbj3ndd699NRC6C0nl3HS+ABeBNaBdTXtWn7+ujxZtXksHVV+r1ioA8hq54AuML4Pf6Pr82iavBBmQbsg166oiniXQN1lVGP3+pY67fSctN7ETqslYHenEpFnSeb+OTvHArWxfSAo4bZ4q7t4jzLklAPZwLEIKgHTx4SCL2JchX4uEjP145Wfw45lb/JCFOHVkUSCkDYUAQ+jxmah4cV8dJQpDo1Uk5uNmAvfLSXnsR2ApCYBDFJGognHfalT61czj2Ezr0JPVoqYiKCofe5w4AZBw4cG+bx64996mmus/Utb3PLsA0P21GTEL09M+6l6ojdWLaEIIQgiyGUaTD/E9Kvgkxknb88z//8+drxk3eve46XQxUA9s+221c24j7l0Tiv3712zMXBulva7rVg8vIdX83IK+L7bZ7Cd9FfJ5sEN0fV/7vJgjb8L3ek9L1rcCrPJPe4vCl7vgu7xVBaM5gO3An2AJQksn6RCTWur/lXvWRlLdcx1M9gWLnqQ5NeBqoSQ2APh+hJEIIiPJJdNRCXyePcMV5PwsdF51EVaHueS6gGXCkWkGMArQBTMdPcn9kbE19GWNxY9rLltCG4eQlGGL09YgMe4a2pwwqHISAUTt5tjoodbdrWZ2oq+xTIE3lf+rbz6bduQc8I/GkXDYTBK939TsrOuOBIGyoF1IewzOwpoZiBGeT+a//+q8vOH8curzaztBr2zzp/w3Wxk4/uybPJIwC10qRTyMp8BTTX3lXYEL92MyN+X2t700N7JcdwTPyVMbiUK8rIN1Y0xLYBbLWrvFr1VITnIuoLHbue1ebljk1hsYaI3D1ZRP2p/TyxLQuuDt0gfsJyL85GvdENOSzFF16Mp5c7z6l5qZ6gLdOF3Fp+8MrIrD16EXYfbdUv7mJzm/vW7C9McqGKYONEPRE0ebexZtPFm2OnAyI8xACvtQB3re5LL/zHN10GyDp64WbyG8hkVM+MAS8SdQ99kfg+OnYA9zANuUjLKlPgJrunPujPgfOeV6oCq6aKedP/uRPPnO6wCz5hetn1M374Xqp4JxTkPozdDeRSl1i9A5B4AmEE8zv2Geoi0hq6iM4nLMPzJ+WcppBcN28WK65OVESh3m3Gx49v5sozTHGdDuTc6ZE8onq0XyJIwH1WMY5oSvSF5gFLr95r4/X3NAuPtueXpsLjE+pMaMZKWul+wSo91rfOl3rc0H2CcM6ryeD9FN7Nw/t6efesie86ifp0e10qVtX/K1K93NNpffaVeELYBdUr4nyNEGe6u9acyjLuW9+S8UbaDv/BfdWFV15N3HyftsEWirxLAOz2DvUQX0MJi+UPMvFVNRJoIS79mxAsxdqu0y2S2eepRqgy+ZumnwjQSRvJ58xGIuPj5jkGfmmPSQH/RagEaKi/f7znHOPua8GnPVZEt1/kpDaURnpw4CzU9+Aa57nEZS65lkxmcT2crgOItcB8kgECCrJyJwIoQzhTV7OTga6jMbCiHPVJUkBq1YLGvuOKtppucbmiluapC7ptUZCVR6JjRHe6Wrciu1YTr5xAOj5kYRY99kUrV/f1Gti8ehi5C5GdddgP7dr17X+tu4WQ9Z22sThqltrI55A+1UfdOr2I/j9O6k1GIu/r9IXbqetMtlO8kzSBcruXwPTKpdu2FLcLn8H5eqUJjJbbufx9P6TYabb/krF1dfbc6Kf2Xfku9e7PQhzb8za+icxGgNlnDcdd8AtHGyuCzttAvH7z0INmOZ9Zw8wTJIGeM1YxLxLsugDfElUJDyRAF6ud3iLrjNDN0+bfNO55928J15SgJRnEcJBhYKgsSN0fwLqlCvuTiSicLdUIjxibMJLSv25hrb+PFxv8k4duLKSuIy942AdF4rIMkqnXu1mmrLyCdGizmIDElwQ0Uhq1Y2+bVDvvQuIL0mq3Wg7LpE10fpwXHVSuwY7UyP1irRgd7mItNmj0Wd7q1OkLdKJeFU8wRqMX3HkLS1fDGKvo2b4linT1s6789/UxGCJVe8f2bV6MasX0Vgpo9/ZZ/v61lkdr355K327L2zGS413oJp67oB2vvJ+6uzOa337u9zOs8tqT4t99vrd38uBdP7bxiv/btcrKnwRrP69BGzFV2UIMZzrgo0FOAJQPGj0HRChw7WAEAbivjIBEe+aXAuICr9go5VxSt6pAwNtQBtwIDohRr1xCUcqXEXyYYvQF7hefS0kRp5p0EsdcfIhFEJq512HAaVuYvmQJBAJxIfKJ2AfAtrtwLWzx7TaDniSvKI20X8dEJCUQN2F0OQ5O6l545jTPIVw5qQRKhZqHM/qq46Uqr8RDEDRNijzrOe26x3Xqk/hy1gLH5L+R/xCFOxGpiLqXfK8v3pdtVtsr5Ou02JQr71rPUkXyLaN5Fqvq2bST+rRxLY58lYDtUTQ5V8SiP+vCJFnrvo2Yep6d320YRnpTacNYV3Olpo1MfB/AXoHR+WuQTUBuyzUezvwShrb/7eue+8iWE/PLxHp1P30lp3jqkeXvb7L2+YeTAAAtAC+xdhRR9uYanyTAKqQ0Q62SRuAAGBQPwtDee6JcIkDdz4CdUd7AvHskR8JRL14u3QIB21VPnVHvgNMybMD4+VZAdaivsA05DsA1sbu9udmBHWwUOoczp1B2KYsexwCiGwfiLG+wvWGg060S/aR5GMvh/LEPOKx1FwwLy//gWqv01bDmZcN9uZWE8b2/Frmq+e3Pm8JRF+wp4QQpl9jjwrh5qiACIUAp26518QY0Vzj8pOh+C2G69VzvZYvnGpC0deaILi/uGBOLmYuLj7hVLdh711Y0HVvhrVVglv2U1mdzp3KV2rJ4KJYrwZqG7ETbvNblZH0SlpYqv5U/h+SroG72rGE6BURu7gH3w38/TEpcWppZ4AGp0p1sdxGwJnenwsoDpqOOws1QJSwEMITEPlbBdWG3/XnT91IJ2IdJeWavRNUC8lXrKPcazUHlY0FFrAX4iHELZy18u19EAcoiYE2qpkQA4QIwQZMvG96Z7NdtGIV5f1c//M///PPJ56FaIZIxKUSQDchQXwcBxqikcQQDTSTN0nDuc0pC2ctmF7eA+wtXQHzlnL1gTkCkFulQRqhy/eM+acM/dHSd0vDkaJ4avHIyifjG9UXItBzOmPJFtM2mLaT7Trrdb9c7hNQLlffHPKTTW/XnPbutcsuumv4KalHSxRPDPe260qNqRced999+PCsBZFOo/I27lUGT5Xsd99DOPZ+cwr+96TdfC6jEW5767QEpNM10Fe79tmdNN2Gp/Z2/TrfXO+FhMMDCvTJ9M/CBCSxAQCAfOiZuW8iCALKsR3Q/ecd3G0+DLt2FlN19CllAtbluSRSBkMzFUoDS4fnFqAOoHIj1a52nwXiPJha2pA3IhlimG87ptPmtJWu245s4TAQJvnpa2c4AEgB2thNwiGnXggJiSvAj5NOeSEkyaddUkWS1Sb2Dr+5drYO/5uZ5/K3GdD50i3h5b3eC4LAf/PN7Q3X81NeSaQQIczzP4xJ6pAxsteCrSLlRA3Hhbe9e5I6MNxqCnq9rO7+FfMoD++0O2hrNi6O/Vr7y3Fv/hf2XNi3BGDL6r5fJnGZ500X6D/h7VM6Wemr0B2cJ8Dcd02IpYYNdtvZ7R/dlLob2xPEe9diWQp5qcM6z63fE9DvJNj6XP3WbVh12/bBNbBAdo2X4hFxPwWkDIkOjefG2dxlQCdgyBbB8AxQ+fHL479/t1uZ54lNUGkbCSR5cqmkWmijN46Q2kbKM0JHI37iDfnfZz6nD4TUAHwpF6gjWqSMJIHs1ItBkwHcJivSjFDY2ZnNnRZhVI9cQ2ComIBQ/tvTYIzEcTJWCLbxRfyps3oNUL+Z58a69fQdrgTQ516HztZ3PKSMg3HRv4hJz2eSGWkRgYtUQIoL+OcZARP1jw2Laxy/JIOe98sgembX8toFeh23qqeJQ3s6LcDj5nttX1jR19secuHI1u9qw+Lmda/7afsOhj4xpE/XTxvCUyFdeXq/pZb7TnfcExHx7Jb//cFtXxOjddld9hKMft/gN3ey97sNryjtEp0f+ty2c/uj+1m9ecXwn289Yutk+c838NJdt3GMWid50J0T+fvoS2GxgbtT0XpRLcAgAGwVNnMhIohQiJK2CQth53BAJteijnBko/z74BkLnzor+fFgyv8QT55M9iQ4Ja4PMLEXgKH85z//+af3U372KZBK0vepI28tEo/jNBFfRC31EmAv77H5kOTadtJnLiMe7bff4L32K88gXCQahNV6YDtotc1ylw2y3jPmIsyGkKaf0vao1Jx3kUTFlf4hQS1evAL1TRf3vPelV+vxAmnjv89s1IAu25pc3FrG8qkNlwqrvy8isvl8+PAl070qs67HU/8+bkwzwZ5evEB7G3oVfuX3lk5v3916eW874Ec/+tHjxGuj3QL1xeVfddnJeP1+IoRt/LkIznIjXWb7i1NtdBsBG6MrTrGNp/TqNpAhIPTnWdgMxeFocbK42N7wFW4Qdwy81Ef/OrjGRjeEQHsElnMWgZ257gFSxmPqB8bYvENFw1vHMwEqxlrg34u2ueQQDHr2tkWQxGxeA9ZJgDH/GZdTFttNygoRYfvQz7muDT1+DLUM2Ow+HAl4GiFq7AHmP/dSfa1MkqWx51mFubjscAtuflNfsUfI/8/+7M8+fbNJ8fiKvUSgQHN0d1mbpzvvtx67xvr3xZTtGrzAdkFzgfl6fzn7fb/XaAP2q7Y84cGW2eOxksCV/3vTGcuowUoBy/1//HiHnb2o9dXRfd/7TwN5Aer17HIyzTUxrMmvF2Y/txNh63px8N3OJYI9gE9teSrj+5HGfHDExmW9Gxr424umPTl6U5iQCEkC2vU5BvqST3w4vwBjvHhyDyfONbPVOryKAqwBiTwnvARJIPdx0FRagAb4BpR52+TbUZX5n2fyybu8m3CkSSSetFs4jaR882gS/TRtCGHCXYu3xAAa9Vq+owpJfghUx3QSmM98JDnkHXp3c868oLKyE9o6ZFfhsYOQMvSbx8rG9QN99SD9pSzSFLsOYoOg91pso7S5g8M3//r8i/S/yKdCmmTeaCsi511z61oHzeB1WmK17z2B7moR9h3jtWqpXbueb6m7++3qv1bzvcLAzs/34k5jyhKZp3zMjQtnO31BEFoPf4FuN7I76ykthe0B7veexMTtkKfneiDf4sh7MXakx7eAu/X+ndcTd7/tfiISLa1cZQJ8wANwOwpnb1vvxd+Tpn37fXe8/d6kxQbQ3CeCKipq8o2xNeoRtgocfcAgzwYghHAgRfCdRyQcqGInKwBngARqUddoQ+pHldXGS+6idOo4aPsHEBiADohzL/UQTjtlON7TfHHMJVtNvkMUuZQakyQEWHRS5VM3sb3kEyKsHtksl3ZZY/abdD14b5E+zCkEpQ347Djmh70fQkxQt/aGtZ57QK/XeWMEJkHe//Zv//bZASCSGWKYPgrB4DK7ANYA2Jzvrqddf9f1p2eWiWui9sQIPmHS3l/OvdU3rfdvx4clXq/KXcazy+w8+/nFTddfEYQvSPNFmS4qfQF9f1+U9Xpvicp27HZq160NQNtJDYhSt8f7LRlcusOnPmjJ4+rDp/YjAMpqA3q3Td4+u4fgvyvcwkoF38x4UPEwLPbmIARF/kCAOE8twEDdm65yL+olz+JMAwAB7xCSEATSBsmC22GHqsj13MeZt9+9DXL85vMM3XXbAXDMvWEMEXPmc94NgaLjzrWAWgArdQkgswmkntlDILx3CIWzIpJn1CB5P0mdgC0pCFes7/MdCQQ46hu2A/UmtVBx6V9jyjCO0OojwNNqinbrNY7qZb+KObTrQF4XE9SMCmYl3kRUb+ZN2miPhfe6PPm2Z1wzn40Hvb6e1p019uHgpvvj+TbAvgLMV1iy1397eB8tTm29r+tbftuNMHnG4UmiMj+73U/pi53KC7adtrEXgO7zT+mixFdFn6jnckZb395o47kVTa9BWRC/uIknIni1pSen1DaT7tOWNpoosAMA7iSEgZTDa6NFfEZIgOP+SoAd7whwA4wkxkFqC7tPqZk6imlSQDPcYZ82BvAEnHOOM1WROETyaSMqiQaXDoRSLl0+wkyFQpJCEEkgf/EXf/E5/lGIFYOovo+EELVQnsn1eBflmQBdvGgk7q5JaZc9CnkmdYlHUY+ZT4gB6aYlHGCtvqQirq048N75zHMn5aQ93F1bXWg+tGpU+frniYtuO2Ib7PsZ3w7J+e677z5LOxlTElMIbva4pOyOrdRrolUwrU5qVc8ykD3vtg1P6QmX3vuePl986TyWeL3Cwq3D4s/3xeC2JuDCq1XDrWTxVl0ej9BcwF3Af6LQnVc36D3pIiqbnzx3EncnJfXk2jquveAJ2F/V7T1cxOa/FLwHzSLxfHMsFjRgo5bBmTWxSmp3xQZX3ETniwOn8w3g8ujJu0AdIDGq2hkbEAj40UXT51NFZfMWFQ7OHaevP4Sb6Lb0GQm5xmXWdfsKkjwTbjd1cV50nonqQlgMnHXy4p4KbEk9OFieSal7VGQpN9FRhfDI8yF8vK4Q4fwP8XCeNA5U2HG2G5vWUk7qwVOMOg3Ak5T0V3vpiBLL9tLzrl2PqZ6MUc9FAKL/m4vkeWXeXGDUHLnQFQF/Bw310auOZsVwLFitZOLetV6V+RbT+QrkL2x6Ijav8O7CD95cSU+qr/cywJdkktRE6eNhWL7+v2pP0hnLaIlC3++CtoD3Av9TpdvI1pNO/t7RAVd+/d3vvVX/pzZf+Xt3J1xP0gbdnuzurZGHWqAne48FTp94C1iAf1K7eiIi+baL2KI2eXD4AEaYYkQEoJEA8t8GLbF+lENlgPPEhQbw1tYBPAAaNRKvKLGZmjMEer+pw3Z64xrJhWoonH4DcMBbkDob8nDU+f+zn/3s9wCYbtypcgKxGSvxmVoUd6wmoqqvUqZ9D8qgumGEl59Q39x+zYUeb+ExbPhrtRFCx8iMsFANNaPUp8DpQ5JEOy00Y2Gu9XxuqTXtDxPgVLwQiA78xzuKfUi5u6YuMF8m9ZK0X63di1m7nrt+7zsX8dqyun3dXxd2bV6vGEv/d49KP7PPvxefv5AQesdnV+jq+KXQb1GfTRcFuzp9gfgqx/tE21ap+L4obZf7pAJb6nq1/Slfz7SfflK7xPZO0lYPuWYhcScEljjpbqvfQlgIT9BHF3Z/WJjUNlw0ETJ+7El26rqfRR9uj3oGkJM6AsB5Plx6L5BcC7HI/aTkQ62VOgQcY3zlbdQqFH2ccnpzWMAzhAABA2DUQTyVRHXl8YRjDXin/O/+P5WHfHH/+Y4qSCTZPMfnnhSVRHXHiEo6wQCwl/RJZ8oSihuQGq+or5IXe0HaJhxHH3ZEQmiimzmQPJ2zwO60DFF7Menf3sG866MZnQZnYxGCkH7+xS9+8dlgHsIb+wy7SKe2nSBCPVfb1tBr41VaAuDa9Zw2vSdPqVW8FwO773n+lbSw+HTls/3dfbXMZL+3+PXU3i9IVQ9Md+orEH4C3VeF7zuvOrN/L6B9bkjpGp+khwXaJXRJnlnw9917ADb//bRRuAfO5KDn78Xq+hqlmijQRxv85tJwqLg8HK9F3gHQGKJz7zd1All8yQVBQ2RtCuOB42hJKg4ALJy1Sam+2tkT1v0AVxK1jciorRJC/NqjCND6xJAJjG34ctIZd1qSTNpj1zDilQSwfvO7YzSTFxtL8kl/9vkMbXdJShkpGwC2fYdkQ7pBjNMO1xACZ10zfLc0mPZzDiAJkHhITb1uU1+SRxLJyfjr89/UyWg8uToGVtujqDD77Gr5py4h6On71F/5jPUdHDDvIOC9rnt9tMtrY9KukbckgAtnFr+enu/P94e04bq5iDHdui2Ar3S05W/q/P1eaaXr03V9amenL5BTJRco+34X+JSud7vS/dz+vvJ/+t0d3RMHF/MEyP2Ns74MZ9sf8rnq3ED11O5WBbW3gHx4g6h/t9WCFHKg8wUsCIf8cfRJFmGX3wSn4/NQU5C2SBNCXSdZsDx87Glo7hE36P3uAwQOENmdy1ZB8gC+fP71BXBMGalXuFLSh/hLCJ5FhABRxwA7hvBw9fm4H2ATlM6Ro7kXWwW3zd7lbJ9GCEc4ZR5N4fD1JxBHWLSDWiztFAsJIRKtNknoEB48ue6IUwSa+kZ/hvDlOsKhzpgPu6N7LiAGPKR6Ax+nAu8ZV2pK53CYb+kvu7etuYxTvttO1fjS9oyLC19J4sKszuuPSU+YeEkhXZ/rmV3XFzPZ7+/vxq+tx1N9to+e0tlLDYgNgFdGT9T5PSLQlV5R/M33GqBWxfSE6t+bp85tUL84lhWfP3z48CYncr3TbdW3za335AMC6oQbteg9A5iAs01lHcOexNDldF9mMVu4+rHDIgQU893eS2wOeSbgl2QXMy4y97mayk+9cs3uYlFU5a1vkp9dw737moRgoxqpBmHLfW6pjOC5B9xFEs31SD8hCoy3iEzuR2JC3PKddpK22q8+7wHr1Cd5haDEnpFrxiXPccF12psAgalv7osB1MSSVIBgyJONRqgRfZZ6Jc8+TEgdkqffCIQ+JRUmUVORzpJIBpiXpN0Ul/ak7fZAZF7ZhY7AJTk8aUPIvFrvu9YW7C+wXTX40/OvyvH9xM0v3vR7V15dt/19PZfUG0zfSl3P92LxuQ9hwXAH5RUHvB3nd7+7Hf5ESa866OxrwJrLXnGy73U+7XXRO0ivSfJK1Ot2ddt7guwAt2jZhOzaAdmp9x/gsJvrpuJolUtS+3lTjSGc2sbQbEctjpPBGGhRHeQ5hmQAFmACjPopIOyMBZxme+YkkYzYPToEQxIjKulAfRlhSSNOOgPc8hZiGlERZroNqDbVudaSTwhl6h41kjMdOnwGA36ISwgCKaI9jBAa4I2jTl2Tb9RMmLA+lCiJ0Vi/YRD0gbmAc+8jRJ1pnDZgIBCZ1K2PYpWP+ZuyWrJs6XZ34PY64XGkrxC2DpWuv3s99bff5nfvt1mG8FqLncdb6en5rdNi4mJT98diZ5dlDfi9dsarbsuYvmrXhTuuP6WPTxl1g75/J3e/Dfih7+3AvpXnEqc22OBEt/N6Evm0G2R76lzlvuJYuvO3/tsXlxHNez2pth5JALCD1nWdGFFx5giG95pIKg/3DrQYMRsQ5BfAxdmzK/TBOIBRu5oz3UicOMgkweSSb96xN4G/vbbwzGkvKVxs8grI5fkOC64ezkLOfYCd1PsqkoSTFgXWsZdCeidR+aR+6Ye0R/3yST/96le/+qxq4iFkLwJVDL/9tCOurVG16J8m+q3DJ2UgrlyGERtMAEO1tpnjyRe3TmLboHMIQMddMrZUPz0HzWdzTCDAnCXxl3/5l5/qxNDO+0r/tYQuLdC2W7L6tdfcvvNDiMBbxGCZyydwbfvYVY+rnMaAtwjEShFtcH9q1ysCs+lRsdad/VYBF8i+J71n0HZwLmK1Fvdcb5fElioMlk59xW08UXptvrgRC0pZ/ZzU4mvr2zufXQDLTQCwlS4YcOmseawA+w5pkHfa1VN76c8tXP1FZ53EVZLxkz+/sA42UPWu5qQ2bPstAFrep+Lh6hkAyfu49tSDIVeEVgRL7BySBMIhTAUgTj6AjKstgz6jp7hJ+jF5OxDHWKZvW0LIdQSG+ks4bACPCCS1yy3ikkTq6rmKYJt7bA6kNXPDb6o3kqJ2i/mEsKlj3iUJWffUSG0gNc9a8m2QEirEHODd9Td/8zefvkmDMTBjIloF8uHQBqhLSyC9xl9hzltM7ROeXdjUZfe1q7zFhieJohP8Wi+q5vSbaLS01mOwZXU938Lcx2inT9c0cjnl/X/lsZTu+r3lNef8RJUXxHXc5t0DsTpF4NgE0LUuZydDc9lb1mW87bbIvwf3qR93YlMNENtzD2cM4Bj8EJFWUSAIJlJvSvJuwCOATNft/ADSAndRXCNQz0dguTwXwqCegEmfdFgNHCtdvbDX4TRj8OVe2h4wuMukAFx7CeU6SQnHbg9CEi4MkBsr6iUEgpopdaBu4dHTqhZgL4Ksc4YZinOdNxKiRuoRtgMnbiyaW0xCFI0tbn2933qPhHttsAfajODaSYpDcJJ6o6O6+E9tRv2jPuZ/q+lio0nZ6RcSHLVkl7fq014nDX6vbJsXEF5p7Q/X2lvbn7R44Np7CcxTnZvoNvHdtjSz2NeXaG2bvv/+BxqVpavTXxGMV5T6U2GH8ecVMWigfGUU6vxXornSSjvL4T+Vv6C9UsGT3WLLa9vFehl1HZ4IQr4zCdrzBEgm4fpw5qQAxEHerYLAceHenTSGe3U6GW4y17KwuZ+23YLbqJ3JAQHcKkDhYaL+bBBJuea8XRvWjD9ul7sosTrgEs663SK7b1pPjliwO5CyABC1EkKYvIEdNRSvG2oaZ1Hry1ar6JeUr99ynd0heeWas6+TnzDcretvbh3Id7RSfaqv9DVQsUbsOaBGIvn1/DZn9DlChaFodVKrWZtzT3uF0qCuE601CRFEMNaGt5KBeu2asTaue7vmr3St2eud5f6f0tO9V3W4JIrGnid8WfXRYuyr9l3p2+vlC+AvCuX/WyD8qpO7vEviMMHfQ2UvUN18PbN60s23qa13WzXVaYlmD2q3p9VU3uudn0+D2XXqSQIM20ib1F46uHmgCGB8uo79Pr91oGtR079b2ECbDt37OFnGSVw6jjDPhGPGHYZ7TN5cLcUbsqs1BAAoC4RHTUXNgfj04mKcpcqJqkLbuWBm81dSCEqSvnKgS94xftRxVFYOxkmKvjzPAddIFHYOM5DnQ7ISl6n3AQBx0WZ5PlH/UcOwUzB6p9xIdCQYIanVmeTVEs//M4f3GKOeG2w+TkhL6g2IgJ9EsOBlVzdPpn/913/9/DxHg3ZA6HwQmA65Yq0gas1NNw41UP7Q1ARor13PKrP/b/r48fcPH7rSe5jCZUqTuq9e5XXd+6Kee6EXU+utZQQYV2y5CMnFXbee/yr3SiZJ572pQdI7T22TX//ufPbZ6/9FcXtT0FXOPt8g3n3a1/xfYtb2BECCO8K9CgBHNcCjBvfaBrBWQ/DAcb6BulKhACS6eGAWIKQHb65eeaKYpt6eJTkEFISLIFm4F9DL7lyqDW1I25Qt79W1tr2l9dsM4amnA4ByPyDuzAR2l+QjNg+QI4U5WF578jz3XMdKJpF0qL7yjJAOxqolgPbwCtBzCe4NYUktCTahsBsa0AN3hvf2OKPiWmBBLBAQexuSj7YyRiMwLZ2RoNLm9Pd33333zU9+8pPPhMoZF4gnTya2G0Zu80ndesOa/73eV/X7SrtwrfP+Xlzo/8swNxZ2/h8/Poe56PwaL3a979pvo7P/+r1x9ir3FUE4JQSpjar93Xr9fffquOveNUhPEkg36pI2VqLYe9uGi5ov8F/lN5W/pAWD0BKA9xBBulBl4Xx20vSEaztDP9euk8CgPUGAgomCQ736xDPAmx0BMFJJOE+ApBAOOt9092wE8qQ64kLJ596JYtRH4hAlD7uhkwIMwjl0gLp8B3ATnrp178mr/f0Z1Om4RSntg1q4tqYtqX84fYbkqHHa3TNulDZtJQ+uqCEi2h7gTr4xQkdyolKy2zsRVCOByTN95dS61CXPG4M8k/by7kLsgHP6gA0i/+XT4A4wqMjYVQAnYEdcmuv2DAKFALB9tNtuby7Tr5LwFT/96U8/9el//ud/ft7kx9CcOdERUq0jxIyka22uGrevL74scPea7XW9wHkxi3vvAt4nhvRVurBtGfRLSlgCskyoOvY7T4z1aUO4VDRPXPlbH43q569O6k59JQVc7+5keEUBG1ibS3plOJIvbtH/pOayfZ4kpSeuwvPNzT31+04Gxsykdp0FLitNrJGuuYpuq8XMC8fh822spk5Km0UQpSZKGYysuc8jyaEz4Q6b+8t7AQOH5LCRiMkEHHgHpT76sHdjM3pf/vjqK08gGe5UvaM+Ihk5c6DjBJFwqFs6LAUClMQ2gMgKXLfjzg01vxmaSV2A2cH0aSMjP2mFOobE1/p50haVEq+v3aOiH3ps9Q9JhP2HLcM8ISk0aLnfYSdIkKnv3//933/z13/915/mQOaU0Bb2jqQNmB359LpVlj7ZtfsKb/p/M2ILoG+98+QZ9cemtzBzMSTpycbQ9zrvi/hJL20If2hjpEuCcH07uSnXe8pYSr6UM2nVXUnNNW2+bwH41vctorWD2mU2179EaSWE1p82RwP8uW32s20vAKhr/OWN03XFsfYhMzjPj79zmxQFVYA5m54QEnl4H4eZugSUoqsXO4iKCXGi/sj7PIoc5JJ3ABbiJOBe96uzDPjvAxi7k3vXNG479QhYOdBH2IxIBPYkUIu0uyMinJQ8cff6gSpJ/yKa3mVfQDjyPGAGqNqYNicInx3LosnKXxwpkgVvqJTVxnZzBxEwH9hher60SzKpSp914L5dJ56jWgzRRSydGhepKfPh3//93z8Dv82Pwof0fO49NG187jW2qYlHz/N+p9fy/u9nn+5fa/6PScsUutbfXX/jeRGlJXqevaSopG+/eUflLhDe/xchuYC0gXGpfOf51OH9f4HT9TYydUfphFX9XJNi292grYxrAq4eVh22n7qMnrRPBKXbqxz1wakpF/fX6iMeKYyVRH/cfPs+kzyShJXAcTYAAA3GZgDX9RGagPEQOFEBcT1UJwZRqp78B4bJq3cmUzEBUbaFgGQ+AZyACpUQkGV7wFUjkqSl9JGYSCQjaiBEzR4NdWC/IC0JfEflZMNd2pTrpAWEN5IJQy1VDClNHCeSkzHVl4gOCQMhzVhRG7bqI/c7rAniRwIwB4wtYsDwa80YN+7PLaG2+yoVnT5P/4W4ez7fsRGRFNIPGb/kaR+Kunb5lyQurarniYFrQnGBbqdX+Txh5A9N15r3/wlvtg6t4TFnXH9LknmTILzVwCfg3t+rGklaQ+pScYO/eQLyznOBdCmnZ1xrDn2TCdf5XsTuInjL5Xf5nW8bvT6MxKF9/UyLxhfxEmjNZiyAjvPrstsYKD8bieje7SBWZoMUcOgTvPJc9OP+OxqyYykB++QfsM61cItZ/ECYcVx8IQDTumrqH/UKVy+cdfJw9kDAGOFqlUbu2wjGGydgah8HQAPs3FkRI5xv8k29xelJHik///NxxKe9EogGwoozT/1/+ctffgL9cNJta0GAAqCpmxPH5K1NiLD2UrngsPVj55HymilCYPIsVZQ2c4HtdSOOk3HpA4xSbvJIIk3Y8CigX55Pe//2b//20/3MiRBGc41k1GttbWDtHuu5lqiXcPRa3WuvQL2x6SIKV3lvpV7DS5iWUbykhf396v8T1m369qmiXYH3cO2dmkJdz7+lt/t+RKG+Jv8F1P6+Gt/XGpQbsNWpB6TL2bo1sdr2PPXVehEoo9vWz16cThPMtg/YTGYDGM6uuXqLm264+6DrxjYAqLUX1yqYmvyoYfJ+755ttUHHx+Fhg5P8UGou3GcIQkce7YWv3VHRdOC75lqBY58ZAWxISwHFpPZACiFAVFK+8BwpC5iROOIWGu5W/0XFlLrR1/cOaJJb++MzggcIY2iNV5Ld20BT6G7Jzuw2jAs+l9+MtfkgnMZMPyHmJAMhtoF8G6c9Z/4ZB8HqJIzG2nAwHyH8+d9OA1RZudeRakWllV/60DnVpNp20LBuViPwCuT7+gXkTxJ7/1buMp0/hJF+wsm+tszy1nEJ30Vknoznnc6dym9Rk7fudcXWyNrPbEdsJ10D4PcO9gX4SU181iAlNSBuWdumvv/Urs3jqW5P95oQLtHZ/mnOrznG7pc29NmNShffkkFzgAhCb2Jiq0hqAyndd4AxnCyCAJCpenDkDLXUPYAjeVGbkHQAFVWMuuBQEaIACndRbqRNHEgb+q138ra6Jx97D3j8MCAnpbxIDCG81BsxkIa71UbtzPuRUpzgJt5SiAbXVHsccOc8lXpjnzkBoKlZzI/kyz5APWYfh70c5orEmAysHRyURO1kPpg7JC22qVaHNlNCVdTqJvs3Avj5HULr/XYkMAdCNGIvscdEH7W77apG/G47Vq+vCzx7PV1r9L3c/pXPD3lu6/bhQRK5MPIJ4BtDO69Xbfr2qbKbnjjeLcz9JQyd73K9++nnnoC4DZby2cZf9ZR6Mi14X5Ol76tbSzHdxidqftWnpYBesNt3ndpO4bn2FnEfZ9VhDsSyyWJrPe+q0HBgrbsVLgIHmGtADrjj8uWb5IS3vINT5oFDtcETCDhTyWTHLo6chMF7iURAxZM628UcQLGPALedRAWCKwVuOFm7iRE0u3ip0PRZj03CY/OIiqqIq6igfNQ5IQ4hBk6Ey/+UlTJDsJI3VZO6AErEm1eO8mxwE0XVe+0+bJ41oANjkgpJgc6ftxNdvrYa1+SDOdj1yhWZ9NXrGnNCKuG5lZTvMBW9n0dIlN6cZlybiWv7YK+3CxCv9IoIvHp38ekJnJ/K2+/Gs7cwLGkxo9u7EkP3zVMdHzemPYH5D2nc3u8yNOaJQ77e2/c7n1afuLcdYCIuiG8CVPvMNUgXIbjq2wBu4nSdr4l0EUvvbMq1NrBa5PqA0a838/SGng0b0DHy87/dDgEwLpHeHrjiYNkbhDjGLXYojYBagAF3y0BpkxpdMi8nqg7qlDaK20egH9gUWori+irmkF3AgvZxN8Wtp77hYOnnU36APBx1pILsKcg9Xk3A0FkF+Q74J4Jprgt0F8kgH3MtKUAf1ZEzEqiBUj9Eyn4OcZmUR+VifweCSyXDY0mfJOkvm/aWmLMF/Xftnm5gxhS1XcQYdZiTdmclRSEmbF8Zk7Qh+xQQFUTM/gR90BJ9fy7Grdfgrq2nNbbvvSIkr8p4b7ow8AlPrrpsX+xGPbjx9H6nlxvTnsBvK/50rcW5zfMSl5bz6ncuSaHTdkJzzlf9+5nmLPra1v2ixvu58pc6r8uOgghtnfa57Q8LlMhOZ5/3cccWbBYq3S8ur3cMd3m9sax3f/Z+CYQIYUCwmpvOPTr5vGN3Ma6UkdMGrA4JgVAokwsoNRWpIWXaC0EVAiAdGpSE6Ij7pJ0kKP0E+HjHiNmffhWLSPjpPG83rnxzneeQXdHJJ3Uk7WTjWlRJATphOIAvAqgcBBZhsvCT0k8O2DF/2l5BLYTgkzp8qM/aGQHR0N/clI05ImBfgnGlMkJQuSZjxn5UMYtCAEiG6ZdsMoy0lRDgaX+Igl3lTrJDEFq91bjzhFHm68V8XQzoMsRvpT+UGFycvOtd99VMLNa9h4B478MPURmtkbUzfSIOe60H4Hq+B64b896yATXudQ2/V736naSNJ9Tlrzrmalc/f9X51YQiml8TId/tD/5EZLuMJigdO4jev10kLWA6chxgruHIWxKxv4AeujkR/ddce67bEUsfLm+goX50vcpgbHQtQMkTKvnSzwMcXihAPt/hJKlKArIBnXDM/P/zP982cFGF4WQd06ld1GYMvaKYsisEjFNm8g0Rs6NYeaSTbheJA+FO2UI8dHgKu7/tbTBmPK6AITWYuUjFk7a0IVa7jAsCYo70/or0t/dTnw6cZ+6aB+ZzUuaTw5WMl7MP8lzKJYFhGvLJwUD/8i//8qm8nJ2QOoagpr8RWyE/2CaSSH7avsynul2OLo0nu75fpQvb9M3TGv0h6T1EqMuAFV1W20thysUAbzoJQmd4gV2DsHcuyiu1kbMrfDWyiUKXs4Rj87ukiquMpaZP1PmprjoZZ/hqwDs/gLnEpSl/E4GLq9E/uD39yhVP32QR4iqbWLSKR9ROwJbf7YrKvx7AcyGVR+cnMmgAK8DY5w3g7EkCiEt7KzF8BjjonXtvAZBGaCx8ZzIAOmChD1Ju6ibYHHCgw+dWGRUQ9VGeD6BFHZQ25Vr0/m3gbWYg+Qaw7TbGDSc5UhQIa1fqieNOMlaYExv9YudpV1XGaifBIfh5p8+PppoRWC71yycpXlE29LWXEimx41OZV+kHu7a926qJZjoQEDu6STgcHqjqHAOa8jgo5HqI7c9+9rNPcyl1bTtL/sfWkrGJbSllcxs2j9XZemvi0UyXtQQn2ualT4zdK8561/DF1F7p4xjkpYsJvdJbzHm73sqnGcwnovPyPIQFydX5d+O+eWiYxbtccN+/fl8A/4pYXQ1sjuGpMy7uoolO53UZj1d3eVHfJpad705QbetJ1SJifzdBagkovztkBDUN3S3OEMcNOCxwn97QRI3gN6Mv7jSJcZAfPS68vZm0kVdM73AFSN7hNqteQJIxmY2EaoU+vRc27jzvOI8gyU5ndQJYjLE2tTVnjzPnuqldHWYh/dXnCtD/M5rrQ4SlXS/t/lYv0kWusQ9E0gk4OmYTcJECGOypa0g+JBf9E2BF6NuI3qBNwmCwNl4ANuX1TmVSGQIZQNdvHXqi97lQXYY4h+jkm6ooYxy7S/KMKkm/YwKSDyLX3HCSgHyM/L3OLrVvX+813mt9JQ+psa3X51up67GG5C5z137Xu9vhezG4cWrzuNLjEZrdyKame/8qfAu7gPOp3P10HbbTpCdisMDf7XhPPle5PfArXbyaCJc4ueVcBHP7eMXfndAWHYCifurolALT9b4Bew0awBlYhZ2mNvJOFiivl7YTALPesESv70xmfvodU4d+mRcJQzjJhVSCsLaxVL+kTrx7ABiXWr71vSiAHi4xefS+iQC1WEI4UO6tiAVbTcrUL+2dpcxw/EnCVHewuj5SM30DaFN+nueZFRDM/ZSFaNqcZ4x7XwgCHqmH2pC3F+IpD/PjYzkiUCNRLQJ+ajGEsj2OhJzo86/9xpwYN0b37L/48Y9//GkOhWClPDYYcyrSQvqQRNX93lihzEsi3zXlf9tUzN997i382rX6Q9ISnVXNvre8C0Ovsp7Sy+B2W8Ba9JuyNSA9fbxjAT8RiX2v67EUecGxO6EH12fL2N9X5zWFNtG8t/V++r+U+WmwXvVF59W+2fqhddbtp03Hbw9A3rOAhQkAEN1Wapy2H+D2uGlm8VInOUYyCZgAMjH0hSygH7ewxUvyvt24dNsICrBO+VEb0OvzkrFJjbdLR+9sCZP+OmAbI6Y8A2ZUSsCNMbP7COjnd+881maAnrLC0UdysudCf1NlCelBYgLuAd1sVosrprMQcMbUb03I8h6CyVbjedJbCGBHKgXOyVtICxIkxoCx3BjkHoKJy9enJBxzIm3s0+BIU5wVSCUpI2cvx9AeyYAURnIxZwUJRLyAuP4330JMei9F48+l0ZBa+9C/32L4ej3v9w9Ji2Fb7hMedNrnF/tepcfgdgYvqVUpntnvVQt9OESafWcb/pTkvfXs313vJ4nk48cvN8l1HdzvDgT+3b73DnRPJEDausPm9rsPk9ZQ9Fb71dtY0fu3nrk9PtgH6HNxjh38rlUv+qZ9/73PyEnSoJsGHPYupBwSRXNzIRLUATyFqKUAU55lewAqATqutQhSEpULQsj+QH9tlzG31dgIopbg1ZQUjjX1CCCHMyVlGDf6dHNFuxEBgIjgCoEh5hDu3dwgASFkABj4kZio+rShAUQIbP3kzGcSBGkr12LHoPZrN1CqRQQ/CSFGwHH25lEb6AWoswYRMRKW8aSq1H7qwhCFEEB2o0gEJASBCc33EBGEUv7GvqPktr3rwqiPD7bG/d9SxvXshXNvpV7fy2Q3o63sCwtXa6Bf19bq/Vfp9+52JkRyma2qZYF93/8hHfGKO1f2PnsRhK1j68RXVaB9r/J9Rem3bleb3jMpLlGvgVg9e18EY+1S/i7fblM+/nTkgBHAEMnXbZe6xvh2TKTmtHI9XJ24OzZkOXpTHvnY3EVHbVdtOELRT5NH9PchDAgP4PaO55prbPdi9WPHYGPoduvXcP/AnGcTNc133333e6GxgVLbPvJcwAsRRGjs6u6AfskLSAa4RSfFZePY5UM6a0N03jE3xDlqKYWO3/kMADrAmXrYE0JllURiSLJ/o4Pd5WO3MddZjAGiTipBOJuokJYQpHZLzT2qvLyXvozrab5jQI501VqFjtNERdjMhXKo96gkW6pfotCM19P6/F+RFnuuOrYk2p6STQwu1dN7GO+k06jMmNXGoM60vwFxi1d/TNrBWuq30sASpO28K/+nzmljbusfLyLRv93v96/ntvxuYy+s5RRaQjPo274tw2LO7zaoMkB2pNFcAxi4QlwpTg6wIkj+J68AukNf2hUyiQFZ3+DckwAEbpenSJ7hZdReSoAKp9mx/5N3B90DqCQSddE2RCR9sxJbSxcIVepgE5f6JIm/kzrQ2+szsYSoyFLH9FW3VehqJ8aFQOGqgSnwDFFoImLN6WNqsJZaELBWtwEJNgqMApBpo7ZNcUKQYAIQuQZq17nohhkJqHeZbCqkV+o9BDTXBN2LVBPp6k//9E8/GYeNobXJAYBNJcnc0m9JcAyxWs75WkP+v5fBfW96Bci7hvtaawH6+uJd4/V7y+307VZordGbmd8XgXgq9ALUbvBy0wvs2zlP+fd733zz2ti8lHTz73Kf8mrCtG16bx5PZe/zxkaeVzs3fxxTc4845CwsXD2ukF48+Ti5CldJfcNATGKwccwO2qhXcGdUCAySqUOHiwY4SdxPHZrSzEDvhehgZxYIQNTmbo9k1zQ3R5JSb86i/si7iCQgzDXP8MAioVBJdKwhwO3sZ1KIHc3yFp6bsT3vBEidf9w++wFInDni2HMZqLaR3H4GoO9satFT01dcUakUEWMgzsOL6lF90n4ht9ks2juNHYT6B/PYOEOd6De1ng17XG3DcPz617/+PSO9uFfexzTYSImRVGYzeVdaHFnm9I9N78UC5V1M36V9WCyw7rqcxamn9nz4vnLOAPzjP/7jCcLN/S4gdSNfFdb5XRXt61t2P9vf/c5Tva7yO7+rztsHO4Cv3v3moa1vpW3TJU1c+T8li4C6yAJpjl9oAxKhRdPiPwBusRy4Mv62Cg74eA73CdzYLyxqoELFoL/25CzSi3qrHxfGPq2MnnttEPng5NteYtNe/ju3gHSSRLpQR/ny5mr1I8AEULy6SA6IMyBFoLncUstQI5EiejOefkDw/TaO9oyQJqgZuCMjGqkXW0I/Y55IrUrGeffcaMaDFInYtzqS+rZVfL1BkWSEoNhLEGwKs0HNRCLMt2vWDEmFjac3a16A+r9rupi+Dx8+vCQInn2FZ9I//MM/fHKeqPTd7xGEr+lr+pq+pq/pf2z67uM3X9PX9DV9TV/T1/TNwz6Er+lr+pq+pq/pf16K5eH/+uZr+pq+pq/pa/qfnv7v/xfB/aaJIcOF5gAAAABJRU5ErkJggg==",
                            BlobName = "VXvgD9+F+iuYCHIn4pZrRyPa+ljB2XHepOJAeUIy39g=.jpeg"
                        });

                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.StatisticRecords.Any())
                {
                    dbContext.StatisticRecords.AddRange(
                        new StatisticRecord
                        {
                            Address = "Kyiv",
                            StreetcodeCoordinateId = 1,
                        },
                        new StatisticRecord
                        {
                            Address = "Lviv",
                            StreetcodeCoordinateId = 1,
                        });

                    await dbContext.SaveChangesAsync();
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}