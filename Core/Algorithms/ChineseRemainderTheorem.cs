using Core.Entities;
using Core.Extensions;

namespace Core.Algorithms;

/// <summary>
/// Provides methods to solve the Chinese Remainder Theorem.
/// </summary>
public class ChineseRemainderTheorem
{
    /// <summary>
    /// Solves the Chinese Remainder Theorem for a given list of terms.
    /// </summary>
    /// <param name="terms">A list of terms, each containing a remainder and a modulus.</param>
    /// <returns>The solution to the Chinese Remainder Theorem for the given terms.</returns>
    public static long Solve(List<ChineseRemainderTheoremTerm> terms)
    {
        // Calculate the product of all moduli
        var productOfModuli = terms.Aggregate(1L, (product, term) => product * term.Modulus);

        // Iterate through each term to calculate the result
        var result = (from term in terms
            let partialProduct = productOfModuli / term.Modulus
            let modularInverse = CalculateModularInverse(partialProduct, term.Modulus)
            select term.Remainder * partialProduct * modularInverse).Sum();

        // Return the result modulo the product of all moduli
        return result.Modulo(productOfModuli);
    }

    /// <summary>
    /// Calculates the modular inverse of a number with respect to a modulus.
    /// </summary>
    /// <param name="number">The number to find the modular inverse of.</param>
    /// <param name="modulus">The modulus.</param>
    /// <returns>The modular inverse of the number with respect to the modulus.</returns>
    /// <exception cref="ArgumentException">Thrown when the number is not invertible.</exception>
    private static long CalculateModularInverse(long number, long modulus)
    {
        long t = 0, newT = 1;
        long r = modulus, newR = number;

        // Apply the Extended Euclidean Algorithm
        while (newR != 0)
        {
            var quotient = r / newR;
            (t, newT) = (newT, t - quotient * newT);
            (r, newR) = (newR, r - quotient * newR);
        }

        // Check if the number is invertible
        if (r > 1) throw new ArgumentException("The number is not invertible");
        if (t < 0) t += modulus;

        return t;
    }
}