using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using TMP_Lab2;

namespace WpfApp2
{
    /// <summary>
    /// Класс, конструктор которого создаёт меню, структура которого определяется данными, находящимися во внешнем текстовом файле.
    /// </summary>
    public class MenuCreator
    {
        /// <summary>
        /// Меню, которое создается конструктором класса.
        /// </summary>
        public Menu CreatedMenu { get; set; }

        /// <summary>
        /// Конструктор, создающий меню из внешнего текстового файла.
        /// Каждая запись файла имеет следующую структуру:
        /// Номер_уровня_в_иерархииПробелНазвание_пунктаПробелСтатус_пунктаПробелИмяМетода
        /// Последовательность записей соответствует последовательности пунктов. Статус пункта определяется: 
        /// 0 – пункт виден и доступен;
        /// 1 – пункт виден, но не доступен;
        /// 2 – пункт не виден.
        /// </summary>
        /// <param name="filename">Имя текстового файла с меню</param>
        public MenuCreator(string fileName)
        {
            CreatedMenu = new Menu();

            string[] lines = File.ReadAllLines(fileName);

            // Создаем массив для элементов меню.
            MenuItem[] menuItems = new MenuItem[lines.Length];

            // Обходим все строки файла.
            for (int i = 0; i < lines.Length; i++)
            {
                // Разбиваем строку на части по пробелам.
                string[] parts = lines[i].Split(' ');

                // Получаем уровень иерархии, название пункта, статус и имя метода.
                int level = int.Parse(parts[0]);
                string name = parts[1];
                int status = int.Parse(parts[2]);
                string? methodName = (parts.Length > 3) ? parts[3] : null;

                // Создаем новый элемент меню по полученным данным.
                MenuItem menuItem = new()
                {
                    Header = name,
                    Tag = methodName
                };

                // Сохраняем элемент в массиве для последующего использования.
                menuItems[level] = menuItem;

                // Если это корневой пункт меню (уровень 0), то добавляем его в меню.
                if (level == 0)
                {
                    CreatedMenu.Items.Add(menuItem);
                }
                // Иначе добавляем его к родительскому элементу меню.
                else
                {
                    MenuItem parentItem = menuItems[level - 1];
                    parentItem.Items.Add(menuItem);
                }

                // Настраиваем пункт меню в зависимости от его статуса.
                // Если пункт недоступен, то отключаем его.
                if (status == 1)
                {
                    menuItem.IsEnabled = false;
                }
                // Если пункт невидим, то скрываем его.
                if (status == 2)
                {
                    menuItem.Visibility = Visibility.Collapsed;
                }

                // Установка обработчика клика мыши.
                if (methodName != null)
                {
                    menuItem.Click += (sender, e) =>
                    {
                        // Вызов метода, связанного с пунктом меню из MenuMethods.cs.
                        Type type = typeof(MenuMethods);
                        MethodInfo method = type.GetMethod(methodName);
                        method?.Invoke(null, null);
                    };
                }
            }
        }
    }
}
