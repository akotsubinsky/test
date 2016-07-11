using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var c1 = new Card() { From = "Мельбурн", To = "Кельн"};
            var c2 = new Card() { From = "Кельн", To = "Москва"};
            var c3 = new Card() { From = "Москва", To = "Париж"};
            var c4 = new Card() { From = "Париж", To = "Милан"};
            var c5 = new Card() { From = "Милан", To = "Ванкувер"};
            var c6 = new Card() { From = "Ванкувер", To = "Лондон" };

            List<Card> cards = new List<Card>() { c3, c2, c5, c1, c6, c4 };

            var sorted = Sort(cards);

            foreach (var card in sorted)
            {
                Console.WriteLine(String.Format("{0} -> {1}", card.From, card.To));
            }

            Console.ReadKey();
        }

        /// <summary>
        ///  сложность O(n)
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        static IEnumerable<Card> Sort(IEnumerable<Card> cards)
        {
            if (cards == null)
                throw new ArgumentNullException();

            int cards_count = cards.Count();
            List<Card> result = new List<Card>(cards_count);

            // два листа from - перечень пунктов отправления
            // to - перечень пунктов назначения
            List<string> from = new List<string>(cards_count);
            List<string> to = new List<string>(cards_count);

            // формируем словать карточек. ключ - пункт отправления карточки. 
            // необходим для поиска последующих карточек за O(1)
            Dictionary<string, Card> dict = new Dictionary<string, Card>(cards_count);

            // наполняем словарь карточек, заодно наполняем перечни пунктов отправления и назначения
            // O(n)
            foreach (var card in cards)
            {
                dict.Add(card.From, card);
                from.Add(card.From);
                to.Add(card.To);
            }

            // таким образом найдем первичный пункт отправления всего маршрута
            var fromPointOfRoute = from.Except(to);
            if (fromPointOfRoute.Count() != 1)
                throw new Exception("Array contains zero or more than 1 from point");

            // это первая карточка
            Card firstCardOfRoute = dict[fromPointOfRoute.Single()];

            result.Add(firstCardOfRoute);
            string nextTo = firstCardOfRoute.To;
            dict.Remove(firstCardOfRoute.From);

            // O(n)
            for (int i = 1; i < cards_count; i++)
            {
                var next = dict[nextTo];
                result.Add(next);
                nextTo = next.To;
            }

            return result;
        }
    }

    class Card
    {
        public string From;
        public string To;
    }
}
