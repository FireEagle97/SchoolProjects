using GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace GeneticAlgorithmTests
{
[TestClass]
public class GenerationTests
{
    //Test EvaluateFitnessOfPopulation method

    [TestMethod]
    public void TestEvaluateFitnessOfPopulation()
    {
        //Arrange
        int populationSize = 10;
        int numberOfGenes = 10;
        int lengthOfGene = 6;
        double mutationRate = 0.1;
        double eliteRate = 0.1;
        int numberOfTrials = 10;
        int seed = 4;
        FitnessEventHandler? fitnessEventHandler = null;
        GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(populationSize, numberOfGenes, lengthOfGene, mutationRate, eliteRate, numberOfTrials, fitnessEventHandler, seed);
        //Act
        Generation generation = (Generation)geneticAlgorithm.GenerateGeneration();
        // generation.EvaluateFitnessOfPopulation();
        //Assert
        Assert.AreEqual(generation, geneticAlgorithm.CurrentGeneration);

    }

}
}