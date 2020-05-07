using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Common.DTOs;
using DataAccess;

namespace Server.Model
{
    public static class CodeProcessing
    {
        //not compared to existing database
        public static async Task GenerateDiscountCodes(int ammount)
        {
            List<DiscountCard> discountCards = new List<DiscountCard>();

            while (ammount > 0)
            {
                for (int i = 0; i < ammount; i++)
                {
                    string code = RandomTextGenerator.Generate(8);
                    var discountCard = new DiscountCard() { DiscountCode = code };
                    discountCards.Add(discountCard);
                }

                discountCards = RemoveCommonCodeFetcher(discountCards);
                ammount -= discountCards.Count;
            }

            var convertedDiscountCards = DiscountCardToDto(discountCards);

            AccessContext ac = new AccessContext();
            await ac.InsertMultipleCodes(convertedDiscountCards);
        }

        private static List<DiscountCard> RemoveCommonCodeFetcher(List<DiscountCard> cards)
        {
            return cards.Distinct().ToList();
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