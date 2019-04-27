using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.Services {
    public class ReviseService : IReviseService {
        private ICardService _cardService;

        public ReviseService(ICardService cardService) {
            _cardService = cardService;
        }

        public async Task<List<Card>> SmartRevise(List<int> packageIds, int num) {
            throw new NotImplementedException();
            var currentDate = DateTime.Today;
            List<Card> cards = new List<Card>();

            foreach (var packageId in packageIds) {
                cards.AddRange((await _cardService.GetAsyncCards(packageId)).Result);
            }

            if (cards.Count <= num)
                return cards;

            return cards.OrderBy(i => i.Proficiency).Take(num).ToList();
        }
    }
}
