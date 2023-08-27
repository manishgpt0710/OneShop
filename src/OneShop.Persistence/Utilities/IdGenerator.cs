using System;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace OneShop.Persistence.Utilities
{
    public class IdGenerator : ValueGenerator<string>
    {
        public static readonly char[] BASE_31_DIGITS = "0123456789BCDFGHJKLMNPQRSTVWXYZ".ToCharArray();
        public static readonly int BASE_SIZE = 31;

        private readonly string _idPrefix;

        public IdGenerator(string idPrefix)
        {
            _idPrefix = idPrefix;
        }

        public override string Next(EntityEntry entry)
        {
            return IdGenerator.NewId(_idPrefix);
        }

        public override bool GeneratesTemporaryValues => false;

        public static string NewId(string prefix)
        {
            //translated from Matrix vb.net to C#
            StringBuilder did = new StringBuilder();

            Guid guid = Guid.NewGuid();
            byte[] guidBytes = guid.ToByteArray();
            long n1 = 0;

            //put bytes in an int64 
            n1 |= guidBytes[7];
            n1 <<= 8;
            n1 |= guidBytes[6];
            n1 <<= 8;
            n1 |= guidBytes[5];
            n1 <<= 8;
            n1 |= guidBytes[4];
            n1 <<= 8;
            n1 |= guidBytes[3];
            n1 <<= 8;
            n1 |= guidBytes[2];
            n1 <<= 8;
            n1 |= guidBytes[1];
            n1 <<= 8;
            n1 |= guidBytes[0];

            //convert to base31
            while (n1 > 0)
            {
                Math.DivRem(n1, BASE_SIZE, out long longresult);
                did.Insert(0, BASE_31_DIGITS[(int)longresult].ToString());
                n1 /= BASE_SIZE;
            }

            //left pad with 0's to 13 chars
            while (did.Length < 13)
            {
                did.Insert(0, "0");
            }

            n1 = guidBytes[8];
            // strip high 3 bits
            n1 &= 31; // CByte("&H1f") same as 31
            // shift left 8 bits
            n1 <<= 8;
            n1 |= guidBytes[9];

            //convert to base31
            while (n1 > 0)
            {
                Math.DivRem(n1, BASE_SIZE, out long longresult);
                did.Insert(0, BASE_31_DIGITS[(int)longresult].ToString());
                n1 /= BASE_SIZE;
            }

            //left pad with 0's to 16 chars
            while (did.Length < 16)
            {
                did.Insert(0, "0");
            }

            string machID = GenerateMachineID();

            did.Insert(0, machID);
            did.Insert(0, prefix);

            return did.ToString();
        }

        private static string GenerateMachineID()
        {
            StringBuilder machId = new StringBuilder();
            Random rnd = new Random();
            machId.Append(BASE_31_DIGITS[rnd.Next(31)]);
            machId.Append(BASE_31_DIGITS[rnd.Next(31)]);
            return machId.ToString();
        }
    }
}
