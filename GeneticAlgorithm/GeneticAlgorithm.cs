﻿using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    internal class GeneticAlgorithm : IGeneticAlgorithm
    {
        private IGeneration _currentGeneration;
        private readonly int _populationSize;
        private readonly int _numberOfGenes;
        private readonly int _lengthOfGene;
        private readonly double _mutationRate;
        private readonly double _eliteRate;
        private readonly int _numberOfTrials;
        private readonly FitnessEventHandler _fitnessCalculation;
        private readonly int? _seed;
        private long _generationCount;


        //Contains a constructor that takes the population size, number of genes, length of genes, mutation rate, elite rate, number of trials, the fitness function, and a potential seed
        public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
        {
            _populationSize = populationSize;
            _numberOfGenes = numberOfGenes;
            _lengthOfGene = lengthOfGene;
            _mutationRate = mutationRate;
            _eliteRate = eliteRate;
            _numberOfTrials = numberOfTrials;
            _fitnessCalculation = fitnessCalculation;
            _seed = seed;
        }

        //implements the interface
        public int PopulationSize => _populationSize;
        public int NumberOfGenes => _numberOfGenes;
        public int LengthOfGene => _lengthOfGene;
        public double MutationRate => _mutationRate;
        public double EliteRate => _eliteRate;
        public int NumberOfTrials => _numberOfTrials;
        public long GenerationCount => _generationCount;
        public IGeneration CurrentGeneration => _currentGeneration;
        public FitnessEventHandler FitnessCalculation => _fitnessCalculation;

        /// <summary>
        /// Generates a generation for the given parameters. If no generation has been created the initial one will be constructed. 
        /// If a generation has already been created, it will provide the next generation.
        /// </summary>
        /// <returns>The current generation</returns>  
        List<Chromosome> chrom = new List<Chromosome>();
        public IGeneration GenerateGeneration()
        {
            if (_currentGeneration == null)
            {
                _currentGeneration = new Generation(new GeneticAlgorithm(_populationSize, _numberOfGenes, _lengthOfGene, _mutationRate, _eliteRate, _numberOfTrials, _fitnessCalculation, _seed), _fitnessCalculation, _seed);
                (_currentGeneration as Generation).EvaluateFitnessOfPopulation();

            }
            else
            {
                _currentGeneration = GenerateNextGeneration();
                (_currentGeneration as Generation).EvaluateFitnessOfPopulation();
                }
            
            _generationCount++;
            return _currentGeneration;
        }

        /// <summary>
        ///This method must create the next set of Chromosomes through reproduction
        /// The elite rate should be used to select only a subset of the best Chromosomes based on fitness - call SelectElites
        /// A new Generation should be created based on the resulting child Chromosomes
        /// </summary>
        /// <returns></returns>
        private IGeneration GenerateNextGeneration()
        {
            var nextGeneration = new Generation(_currentGeneration);
    
    //create a empty new generation
            // var newGeneration = new Generation(this,FitnessCalculation, _seed);
            //          int[] genesnew1 = newGeneration.ChromosomesArray[0].Genes;
            // int[] genesnew2 = newGeneration.ChromosomesArray[1].Genes;
            // Console.WriteLine("New Generation before changes ...........................");
            //     for (int i = 0; i < genesnew1.Length; i++)
            // {
            //     Console.Write(genes1[i] + " ");
            // }
            // Console.WriteLine();
            // for (int i = 0; i < genesnew2.Length; i++)
            // {
            //     Console.Write(genes2[i] + " ");
            // }


            //start to populate after the elites
            for (var i =0; i < PopulationSize; i++){
                    var parent1 = nextGeneration.SelectParent();
                    var parent2 = nextGeneration.SelectParent();
                    //if parent1 and parent2 fitness are the same, then select a new parent2
                        //while (parent1.Fitness == parent2.Fitness){
                            //parent2 = nextGeneration.SelectParent();
                        //}
                    var childrenGeneration = parent1.Reproduce(parent2, MutationRate);
                    //add the reproduced children to the ChildChromosomes
                    //     if (i == PopulationSize -1){
                    //     break;
                    // }
                   

                    nextGeneration[i] = childrenGeneration[0];
                    nextGeneration[i+1] = childrenGeneration[1];
                    i++;    
                }
       
            return nextGeneration;
        }
    }
}
