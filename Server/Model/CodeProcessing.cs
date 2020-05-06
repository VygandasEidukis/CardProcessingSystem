using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Common.DTOs;
using DataAccess;

namespace Server.Model
{
    public static class CodeProcessing
    {
        public static async Task GenerateDiscountCodes(int ammount)
        {
            List<DiscountCard> discountCards = new List<DiscountCard>();

            for (int i = 0; i < ammount; i++)
            {
                string code = RandomTextGenerator.Generate(8);
                var discountCard = new DiscountCard() { DiscountCode = code };
                discountCards.Add(discountCard);
            }

            var convertedDiscountCards = DiscountCardToDto(discountCards);

            AccessContext ac = new AccessContext();
            await ac.InsertMultipleCodes(convertedDiscountCards);
        }

        private static List<DiscountCard> CommonCodeFetcher(List<DiscountCard> cards)
        {
            var repeatingCards = new List<DiscountCard>();

            foreach (var discountCard in cards)
            {
                int exists = 0;
                foreach (var card in cards)
                {
                    if (card.DiscountCode == discountCard.DiscountCode)
                        exists++;
                }

                if(exists > 1)
                    repeatingCards.Add(discountCard);
            }

            return repeatingCards;
        }

        private static List<DiscountCardDto> DiscountCardToDto(List<DiscountCard> convert)
        {
            var convertedDiscountCards = new List<DiscountCardDto>();

            foreach (var discountCard in convert)
            {
                convertedDiscountCards.Add(discountCard.ToDto());
            }

            return convertedDiscountCards;
        }
    }
}