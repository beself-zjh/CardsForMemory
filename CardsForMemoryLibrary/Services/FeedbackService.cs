using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsForMemoryLibrary.Services {
    public class FeedbackService : IFeedbackService {
        /// <summary>
        ///     卡片服务
        /// </summary>
        private ICardService _cardService;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="cardService"></param>
        public FeedbackService(ICardService cardService) {
            _cardService = cardService;
        }

        public async Task isEasy(Card card) {
            await Utility(card, Level.Easy);
        }

        public async Task isNormal(Card card) {
            await Utility(card, Level.Normal);
        }

        public async Task isDifficult(Card card) {
            await Utility(card, Level.Difficult);
        }

        private async Task Utility(Card card, Level level) {
            _cardService.BeOld(card.Id);//记录下该卡片已经复习过
            var updateCard = new Card() {
                Id = card.Id,
                Answer = card.Answer,
                Options = card.Options,
                PackageId = card.PackageId,
                Proficiency = card.Proficiency,
                Question = card.Question,
                UpdateTime = DateTime.Now
            };
            switch (level) {
                case Level.Easy:
                    updateCard.Proficiency += 200;
                    break;
                case Level.Normal:
                    updateCard.Proficiency += 100;
                    break;
                case Level.Difficult:
                    break;
                default:
                    //随便抛了个不知名异常
                    throw new KeyNotFoundException();
            }

            if (updateCard.Proficiency > 10000)
                updateCard.Proficiency = 10000;
            await _cardService.EditCardAsync(updateCard);
        }

        public bool Submit(Card card, string answer) {
            throw new NotImplementedException();
        }
    }

    
}
