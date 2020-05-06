using Common.DTOs;
using Common.Interfaces;

namespace Server.Model
{
    public class DiscountCard : IDiscountable, ICard
    {
        public int ID { get; set; }
        public string DiscountCode { get; set; }

        public DiscountCardDto ToDto()
        {
            return new DiscountCardDto()
            {
                ID = ID,
                DiscountCode = DiscountCode
            };
        }

        public override string ToString()
        {
            return DiscountCode;
        }
    }
    
}