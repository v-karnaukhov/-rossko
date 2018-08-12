using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using PermutationsService.Data.ServicesData;
using PermutationsService.Services.Abstract;

namespace PermutationsService.Services.Concrete
{
    public class PermutationsService : IPermutationsService
    {
        private static readonly HashAlgorithm s_hashAlgorithm = SHA256.Create();

        public PermutationEntry GetPermutations(string element)
        {
            var elementUniqueKey = GetUniqueKeyByValue(element);
            var strBuilder = new StringBuilder();
            var timer = new Stopwatch();

            timer.Start();
            Permute(element.ToCharArray(), 0, element.Length - 1, strBuilder);
            timer.Stop();

            return new PermutationEntry
            {
                Item = element,
                Result = strBuilder.ToString(),
                UniqueKey = elementUniqueKey,
                SpendedTime = timer.Elapsed.ToString()
            };
        }

        public string GetUniqueKeyByValue(string element)
        {
            return string.IsNullOrEmpty(element)
                ? string.Empty
                : GetHashString(GetHash(element));
        }

        private IEnumerable<byte> GetHash(string element)
        {
            return s_hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(element));
        }

        private string GetHashString(IEnumerable<byte> hashBytes)
        {
            var strBuilder = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                strBuilder.Append(hashByte.ToString("X2"));
            }

            return strBuilder.ToString();
        }

        private void Swap(ref char a, ref char b)
        {
            if (a == b)
                return;
            a ^= b;
            b ^= a;
            a ^= b;
        }

        private void Permute(char[] elements, int recursionDepth, int maxDepth, StringBuilder stringBuilder)
        {
            if (recursionDepth == maxDepth)
            {
                if (stringBuilder.Length == 0)
                {
                    stringBuilder.Append(elements);
                }
                else
                {
                    stringBuilder.Append(", ");
                    stringBuilder.Append(elements);
                }

                return;
            }

            for (var i = recursionDepth; i <= maxDepth; i++)
            {
                Swap(ref elements[recursionDepth], ref elements[i]);
                Permute(elements, recursionDepth + 1, maxDepth, stringBuilder);
                Swap(ref elements[recursionDepth], ref elements[i]);
            }
        }
    }
}
