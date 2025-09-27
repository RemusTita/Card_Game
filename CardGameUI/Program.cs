using System;
using System.Collections.Generic;
using System.Linq;
using CardGameUI.Models;

namespace CardGameUI
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            Blackjack poker = new Blackjack();

            var hand = poker.DealCards();

            foreach (var card in hand)
            {
                Console.WriteLine($"{card.Value.ToString()} {card.Suit.ToString()}");
            }
            
            Blackjack blackjack = new Blackjack();
        }
    }

    public abstract class Deck
    {
        private List<PlayingCardModel> fullDeck = new List<PlayingCardModel>();
        private List<PlayingCardModel> drawPile = new List<PlayingCardModel>();
        protected List<PlayingCardModel> discardPile = new List<PlayingCardModel>();

        protected void CreateDeck() 
        {
            fullDeck.Clear();
            
            for (int suit = 0; suit < 4; suit++)
            {
                for (int value = 1; value < 14; value++)
                {
                    fullDeck.Add(new PlayingCardModel
                    {
                        Suit = (CardSuit)suit,
                        Value = (CardValue)value
                    });
                }
            }
        }

        protected void ShuffleDeck()
        {
            var rand = new Random();
            drawPile = fullDeck.OrderBy(x => rand.Next()).ToList();
        }
        
        

        public abstract List<PlayingCardModel> DealCards();

        protected virtual PlayingCardModel DrawOneCard()
        {
            PlayingCardModel output = drawPile.FirstOrDefault();
            drawPile.Remove(output);

            return output;
        }
    }

    public class Poker : Deck
    {

        public Poker()
        {
            CreateDeck();
            ShuffleDeck();
        }
        
        public override List<PlayingCardModel> DealCards()
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();

            for (int i = 0; i < 5; i++)
            {
                output.Add(DrawOneCard());
                
            }

            return output;
        }

        /*public List<PlayingCardModel> RequestCards(List<PlayingCardModel> cardsToDiscard)
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();

            foreach (var card in cardsToDiscard)
            {
                output.Add(DrawOneCard());
                discardPile.Add(card);
            }

            return output;
        }*/

    }
    
    public class Blackjack : Deck
    {

        public Blackjack()
        {
            CreateDeck();
            ShuffleDeck();
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
        
        /*public PlayingCardModel RequestCard()
        {
            return DrawOneCard();
        }*/
    }
}