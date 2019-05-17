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
            _cardService.BeOld(card.Id);//记录下该卡片已经复习过
            card.UpdateTime = DateTime.Now;
            card.Proficiency += 200;
            if (card.Proficiency > 10000)
                card.Proficiency = 10000;
            await _cardService.EditCardAsync(card);
        }

        public async Task isNormal(Card card) {
            _cardService.BeOld(card.Id);//记录下该卡片已经复习过
            card.UpdateTime = DateTime.Now;
            card.Proficiency += 100;
            if (card.Proficiency > 10000)
                card.Proficiency = 10000;
            await _cardService.EditCardAsync(card);
        }

        public async Task isDifficult(Card card) {
            _cardService.BeOld(card.Id);//记录下该卡片已经复习过
            card.UpdateTime = DateTime.Now;
            if (card.Proficiency > 10000)
                card.Proficiency = 10000;
            await _cardService.EditCardAsync(card);
        }


        public bool Submit(Card card, string answer) {
            throw new NotImplementedException();
        }
    }

    
}
