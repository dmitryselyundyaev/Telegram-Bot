using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotCode
{   
  
    class Program
    {
        private static string token = "";
        private static TelegramBotClient client;
        static void Main(string[] args)
        {

            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += onMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }
        private static async void onMessageHandler(object sender, MessageEventArgs e)
        {
            string StepRns, StandofRns, TaperRns, InsertRns;
            Answers(out StepRns, out StandofRns, out TaperRns, out InsertRns, out string Mzs1, out string Mzs2, out string Mzs3, out string Mzs4);
            var msg = e.Message;
            if (msg.Text == "/start")
            {
                await client.SendTextMessageAsync(msg.Chat.Id, "Ожидание выбора", replyMarkup: MainButtons());
            }
            switch (msg.Text)
            {
                case "МЗС":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Что вас интересует?", replyMarkup: MainMenuMzs());
                    break;
                case "РНС":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Что вас интересует?", replyMarkup: MainMenuRns());
                    break;
                case "Как исправить подрез?":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, StepRns);
                    await client.SendPhotoAsync(
                        chatId: e.Message.Chat.Id,
                        photo: "https://github.com/dmitryselyundyaev/Telegram-bot-for-cnc-operator/blob/master/ImgSource/%D0%9F%D0%BE%D0%B4%D1%80%D0%B5%D0%B7.jpg?raw=true",
                        caption: "Визуальный пример",
                        parseMode: ParseMode.Html);
                    break;
                case "Как поменять натяг?":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, StandofRns);
                    break;
                case "Как исправить конусность?":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, TaperRns);
                    break;
                case "Как поменять гребенку?":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, InsertRns);
                    break;
                case "Позвонить наладчику":
                    Thread.Sleep(2000);
                    await client.SendContactAsync(
                     chatId: e.Message.Chat.Id,
                     phoneNumber: "+79108926865",
                     firstName: "Максим",
                     lastName: "Петрович");
                    Thread.Sleep(500);
                    await client.SendContactAsync(
                     chatId: e.Message.Chat.Id,
                     phoneNumber: "+79108926088",
                     firstName: "Антон",
                     lastName: "Сергеевич");
                    break;
                case "Параметры для перевалки РНС":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Укажите тип резьбы",replyMarkup: ProductTypeChangeRns());
                    break;
                case "Параметры для перевалки МЗС":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Укажите тип резьбы", replyMarkup: ProductTypeChangeMzs());
                    break;
                case "Номера ремонтных служб":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, "+79234423982 Механники \r\n+79234423951 Электрики \r\n+79239113339 Автоматчики", replyMarkup: MainButtons());
                    break;
                case "FAQ по базовым операциям РНС":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Выберите требую коррекцию", replyMarkup: FaqButtonsRns());
                    break;
                case "В главное меню":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Вы в главном меню", replyMarkup: MainButtons());
                    break;
                case "FAQ по базовым операциям МЗС":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Выберите требую коррекцию", replyMarkup: FaqButtonsMzs());
                    break;
                case "Как скорректировать расстояние до треугольника?":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, Mzs1);
                    break;
                case "Теория коррекции на премиум соединениях":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, Mzs2);
                    break;
                case "Что делать если закусило муфту?":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, Mzs3);
                    break;
                case "Регулировка стола":
                    Thread.Sleep(500);
                    await client.SendTextMessageAsync(msg.Chat.Id, Mzs4);
                    break;

            }
        }
        private static void Answers(out string StepRns, out string StandofRns, out string TaperRns, out string InsertRns , out string Mzs1, out string Mzs2, out string Mzs3, out string Mzs4)
        {
            StepRns = "Перейдите в экран 'Macro' найдите параметр под номером '536'" +
            " и измените его значение в зависимости от строны подреза .Если подрез находится со стороны оператора то " +
            "от имеющегося числа отнимаем приблизительно 0.02 ,если подрез находится с внутренней стороны то прибавляем 0.02. ";
            StandofRns = "Перейдите в экран 'Macro' найдите параметр под номером '511'  и измените его ." +
" Учтите что 1мм фактического натяга соответсвет 0.06мм от значения параметра,, для изменения увеличьте/убавьте данный " +
"параметр на нужное вам значение";
            TaperRns = "Перейдите в экран 'Macro' найдите параметр под номером '537'  и измените его " +
".Учтите что 0.05мм конусности соответсвет 0.0010мм от значения параметра, для изменения " +
"увеличьте/убавьте данный параметр на нужное вам значение ";
            InsertRns = "Открутите прижимной винт шестигранным ключем на 5 , продуйте посадочное место под гребенку и установите новую прижав ее в обратную от шпинделя сторону";
            Mzs1 = "Тут пока ничего нет , но скоро будет";
            Mzs2 = "Тут пока ничего нет , но скоро будет";
            Mzs3 = "Тут пока ничего нет , но скоро будет";
            Mzs4 = "Тут пока ничего нет , но скоро будет";

        }

        private static IReplyMarkup FaqButtonsRns()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton { Text = "Как исправить подрез?"}, new KeyboardButton { Text = "Как поменять натяг?"} } ,
                    new List<KeyboardButton>{new KeyboardButton { Text = "Как исправить конусность?"}, new KeyboardButton { Text = "Как поменять гребенку?"} },
                    new List<KeyboardButton>{new KeyboardButton { Text = "В главное меню"}},

                }
            };
        }
        private static IReplyMarkup FaqButtonsMzs()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton { Text = "Как скорректировать расстояние до треугольника?"}, new KeyboardButton { Text = "Теория коррекции на премиум соединениях"} } ,
                    new List<KeyboardButton>{new KeyboardButton { Text = "Что делать если закусило муфту?"}, new KeyboardButton { Text = "Регулировка стола"} },
                    new List<KeyboardButton>{new KeyboardButton { Text = "В главное меню"}},

                }
            };
        }
        private static IReplyMarkup MainMenuRns()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton { Text = "FAQ по базовым операциям РНС"}},
                    new List<KeyboardButton>{new KeyboardButton { Text = "Параметры для перевалки РНС"}},
                    new List<KeyboardButton>{new KeyboardButton { Text = "В главное меню"}}

                }
            };
        }
        private static IReplyMarkup MainMenuMzs()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton { Text = "FAQ по базовым операциям МЗС"}},
                    new List<KeyboardButton>{new KeyboardButton { Text = "Параметры для перевалки МЗС"}},
                    new List<KeyboardButton>{new KeyboardButton { Text = "В главное меню"}}

                }
            };
        }
        private static IReplyMarkup MainButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = "РНС" }, new KeyboardButton { Text = "МЗС" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Позвонить наладчику" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Номера ремонтных служб" } }

                }
            };
        }
        private static IReplyMarkup ProductTypeChangeRns()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = "Батресс - РНС" }, new KeyboardButton { Text = "ОТТМ - РНС" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "ВМЗ - 1 - РНС" }, new KeyboardButton { Text = "ОТТГ - РНС" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Треугольная резьба - РНС" }, new KeyboardButton { Text = "ВМЗ-4 - РНС" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "В главное меню" } }
                }
            };
        }
        private static IReplyMarkup ProductTypeChangeMzs()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = "Батресс - МЗС" }, new KeyboardButton { Text = "ОТТМ - МЗС" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "ВМЗ-1 - МЗС" }, new KeyboardButton { Text = "ОТТГ - МЗС" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Треугольная резьба - МЗС" }, new KeyboardButton { Text = "ВМЗ-4 - МЗС" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "В главное меню" } }
                }
            };
        }

    }
}
