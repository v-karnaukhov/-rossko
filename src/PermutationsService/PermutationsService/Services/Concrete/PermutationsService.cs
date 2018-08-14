using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PermutationsService.Services.Abstract;
using PermutationsService.Web.DataAccess.Abstract;
using PermutationsService.Web.DataAccess.Concrete;
using PermutationsService.Web.DataAccess.Entities;

namespace PermutationsService.Web.Services.Concrete
{
    public class PermutationsService : IPermutationsService
    {
        private static readonly HashAlgorithm s_hashAlgorithm = SHA256.Create();
        private static readonly object s_lock = new object();

        public async Task<PermutationEntry> GetPermutations(string element)
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var elementUniqueKey = GetUniqueKeyByValue(element);
                var permutationEntryFromDB = await unitOfWork.PermutationsRepository
                    .FindBy(x => x.UniqueKey == elementUniqueKey).FirstOrDefaultAsync();

                if (permutationEntryFromDB != null)
                {
                    return permutationEntryFromDB;
                }

                var strBuilder = new StringBuilder();
                var timer = new Stopwatch();

                timer.Start();
                Permute(element.ToCharArray(), 0, element.Length - 1, strBuilder);
                timer.Stop();

                var resultString = strBuilder.ToString();
                var resultEntry = await unitOfWork.PermutationsRepository.AddAsync(new PermutationEntry
                {
                    Item = element,
                    ResultString = resultString,
                    ResultCount = resultString.Count(x => x == ',') + 1,
                    UniqueKey = elementUniqueKey,
                    SpendedTime = timer.Elapsed.ToString()
                });

                await unitOfWork.SaveAsync();

                return resultEntry;
            }
        }

        /// <summary>
        /// Костыльное решение, чтобы иметь возможность подменять UnitOfWork без прокидывания доп. зависимостей.
        /// </summary>
        public virtual IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork();
        }

        public async Task<IEnumerable<PermutationEntry>> GetPermutations(string[] elements)
        {
            var result = new ConcurrentBag<PermutationEntry>();
            await Task.WhenAll(elements.Select(s => Task.Run(async () =>
            {
                var permutationResult = await GetPermutations(s);
                result.Add(permutationResult);
            })));

            return result.ToList();
        }

        public string GetUniqueKeyByValue(string element)
        {
            lock (s_lock)
            {
                return string.IsNullOrEmpty(element)
                    ? string.Empty
                    : GetHashString(GetHash(element));
            }

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
