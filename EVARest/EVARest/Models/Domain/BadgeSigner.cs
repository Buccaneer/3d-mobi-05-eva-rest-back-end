using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EVARest.Models.Domain {
    public class BadgeSigner {
        private static BadgeSigner _instance;
        private IDictionary<string, IBadgeRewarder> _rewarders;

        private BadgeSigner() {
            var currentAssembly = GetType().Assembly;
            _rewarders = new Dictionary<string, IBadgeRewarder>();
            foreach (var t in BadgeAwarder.GetTypesWithHelpAttribute(currentAssembly)) {
                var instance = (IBadgeRewarder)Activator.CreateInstance(t.InstanceType);
                _rewarders[t.Name] = instance;

            }
        }

        public static BadgeSigner Instance {
            get {
                if (_instance == null)
                    _instance = new BadgeSigner();

                return _instance;
            }

        }

        public void RewardBadges(ApplicationUser user, Challenge current) {
            foreach (var kv in _rewarders) {

                if (user.HasBadge(kv.Key))
                    kv.Value.ChangeBadge(user.GiveBadge(kv.Key), user, current);
                else {
                    var newBadge = kv.Value.GenerateNewBadge(user, current);
                    if (newBadge != null)
                        user.Badges.Add(newBadge);
                }
            }
        }

    }

    public class BadgeAwarder : Attribute {
        public string Name { get; set; }
        public Type InstanceType { get; set; }

        public BadgeAwarder(string name) {
            Name = name;
        }

        public static IEnumerable<BadgeAwarder> GetTypesWithHelpAttribute(Assembly assembly) {
            foreach (Type type in assembly.GetTypes()) {
                if (type.GetCustomAttributes(typeof(BadgeAwarder), true).Length > 0) {
                    var attr = (BadgeAwarder)type.GetCustomAttribute(typeof(BadgeAwarder), true);
                    attr.InstanceType = type;
                    yield return attr;
                }
            }
        }
    }



    public interface IBadgeRewarder {
        void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge);
        Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge);
    }

    [BadgeAwarder("starter")]
    public class StarterBadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {

        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            if (user.Challenges.Count == 1 && currentChallenge.Done == false)
                return new Badge() {
                    Name = "starter",
                    Description = "You requested your first challenge!"
                };

            return null;
        }
    }

    [BadgeAwarder("doorzetter")]
    public class DoorzetterBadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {

        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            if (user.Challenges.Count(c => c.Done) == 21)
                return new Badge() {
                    Name = "doorzetter",
                    Description = "  "
                };
            return null;
        }
    }

    [BadgeAwarder("level1")]
    public class Level1BadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {
            if (user.Points >= 5)
                badge.Name = "level2";
        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            var levels = new string[] { "level1", "level2", "level3", "level4", "level5", "level6" };
            foreach (var level in levels)
                if (user.HasBadge(level))
                    return null;
            if (user.Points == 0)
                return new Badge() { Name = "level1", Description = "Level one reached." };
            return null;
        }
    }

    [BadgeAwarder("level2")]
    public class Level2BadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {
            if (user.Points >= 10)
                badge.Name = "level3";
        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            //  throw new NotImplementedException();
            return null;
        }
    }

    [BadgeAwarder("level3")]
    public class Level3BadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {
            if (user.Points >= 20)
                badge.Name = "level4";
        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            //  throw new NotImplementedException();
            return null;
        }
    }

    [BadgeAwarder("level4")]
    public class Level4BadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {
            if (user.Points >= 32)
                badge.Name = "level5";
        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            //  throw new NotImplementedException();
            return null;
        }
    }

    [BadgeAwarder("level5")]
    public class Level5BadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {
            if (user.Points >= 50)
                badge.Name = "level6";
        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            //  throw new NotImplementedException();
            return null;
        }
    }

    [BadgeAwarder("trotsegebruiker")]
    public class TrotseGebruikerBadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {
        
        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            if (user.Challenges.Count(c => c.Done) >= 10)
                return new Badge() { Name = "trotsegebruiker", Description = "    " };
            return null;
        }
    }

    [BadgeAwarder("genieter")]
    public class GenieterBadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {
          
        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            //  throw new NotImplementedException();
            if (user.Challenges.Count(c => c is RestaurantChallenge) == 1 && currentChallenge is RestaurantChallenge && currentChallenge.Done)
                return new Badge() { Name = "genieter", Description = "      " };
            return null;
        }
    }

    [BadgeAwarder("gastronoom")]
    public class GastronoomBadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {

        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            //  throw new NotImplementedException();
            if (user.Challenges.Count(c => c is RestaurantChallenge && c.Done) == 3 && currentChallenge is RestaurantChallenge && currentChallenge.Done)
                return new Badge() { Name = "gastronoom", Description = "            " };
            return null;
        }
    }

  //  [BadgeAwarder("gastheer")]
    public class GastheerBadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {

        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            //  throw new NotImplementedException();
            if (user.Challenges.Count(c => c is RecipeChallenge && c.Done && (c as RecipeChallenge).PrepareFor != TargetSubject.Self) == 1 && currentChallenge is RecipeChallenge && currentChallenge.Done && (currentChallenge as RecipeChallenge).PrepareFor != TargetSubject.Self)
                return new Badge() { Name = "gastheer", Description = "            " };
            return null;
        }
    }

    [BadgeAwarder("explorer")]
    public class ExplorerBadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {

        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            //  throw new NotImplementedException();
            if (user.Challenges.Count(c => c is RecipeChallenge && c.Done && (c as RecipeChallenge).Recipe.Properties.Count(p => p.Type.Equals("Regio")) >= 1) == 1 && currentChallenge is RestaurantChallenge && currentChallenge.Done)
                return new Badge() { Name = "explorer", Description = "            " };
            return null;
        }
    }

    [BadgeAwarder("creatieveling")]
    public class CreatievelingBadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {

        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            //  throw new NotImplementedException();
            if (user.Challenges.Count(c => c is CreativeCookingChallenge && c.Done) == 1 && currentChallenge is CreativeCookingChallenge && currentChallenge.Done)
                return new Badge() { Name = "creatieveling", Description = "            " };
            return null;
        }
    }


    [BadgeAwarder("sugarrush")]
    public class SugarRushBadgeRewarder : IBadgeRewarder {
        public void ChangeBadge(Badge badge, ApplicationUser user, Challenge currentChallenge) {

        }

        public Badge GenerateNewBadge(ApplicationUser user, Challenge currentChallenge) {
            //  throw new NotImplementedException();
            if (user.Challenges.Count(c => c is TextChallenge && c.Done && c.Type.Equals("Suikervrij")) == 1 && currentChallenge is TextChallenge && currentChallenge.Done && currentChallenge.Type.Equals("Suikervrij"))
                return new Badge() { Name = "sugarrush", Description = "            " };
            return null;
        }
    }
}
