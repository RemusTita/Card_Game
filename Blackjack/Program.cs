using System;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Models;

namespace Blackjack
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            Poker pokerDeck = new Poker();
            var hands = pokerDeck.DealCards();

            foreach (var item in hands) 
            {
                Console.WriteLine($"{item.Value} {item.Suit}");
            }
        }
    }

    public abstract class Deck
    {
        private List<PlayingCardModel> fullDeck = new List<PlayingCardModel>(); 
        // private List<PlayingCardModel> drawCards = new List<PlayingCardModel>();
        protected List<PlayingCardModel> discardCards = new List<PlayingCardModel>();

        protected void CreateDeck()
        {
            
            fullDeck.Clear();
            
            for (int suit = 0; suit < 4; suit++)
            {
                for (int value = 0; value < 13; value++)
                {
                    fullDeck.Add(new PlayingCardModel
                    {
                        Suit = (CardSuit)suit,
                        Value = (CardValue)value
                    });
                }
            }
            ShuffleDeck();
        }

        private void ShuffleDeck()
        {
            Random rnd = new Random();
            // drawCards = fullDeck.OrderBy(item => rnd.Next()).ToList();
            for (int n = fullDeck.Count - 1; n > 0; n--)
            {
                int k = rnd.Next(n + 1);
                (fullDeck[n], fullDeck[k]) = (fullDeck[k], fullDeck[n]);
            }
            
            /*
             for (int n = deck.Length - 1; n > 0; --n)
            {
                int k = r.Next(n+1);
                int temp = deck[n];
                deck[n] = deck[k];
                deck[k] = temp;
            }
            */
        }

        public abstract List<PlayingCardModel> DealCards();

        protected PlayingCardModel DrawOneCard()
        {
            PlayingCardModel output = fullDeck.FirstOrDefault();
            fullDeck.Remove(output);

            return output;
        }

    }

    public class Poker : Deck
    {
        public Poker()
        {
            CreateDeck();
        }
        
        public override List<PlayingCardModel> DealCards()
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();

            for (int i = 0; i < 52; i++)
            {
                output.Add(DrawOneCard());
            }

            return output;
        }

        public List<PlayingCardModel> RequestCards(List<PlayingCardModel> cardsToDiscard)
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();

            foreach (var card in cardsToDiscard)
            {
                output.Add(DrawOneCard());
                discardCards.Add(card);
            }

            return output;
        }

    }
    
    public class Blackjack : Deck
    {
        public Blackjack()
        {
            CreateDeck();
        }
        
        public override List<PlayingCardModel> DealCards()
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();

            for (int i = 0; i < 2; i++)
            {
                output.Add(DrawOneCard());
            }

            return output;
        }

        public PlayingCardModel RequestCard()
        {
            return DrawOneCard();
        }
    }
}