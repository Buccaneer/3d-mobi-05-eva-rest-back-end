using EVARest.Models.DAL;
using EVARest.Models.Domain;
using EVARest.Models.Domain.I18n;
using EVARest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EVARest.Controllers
{
    /// <summary>
    /// The challenge resource.
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Challenges")]
    public class ChallengeController : ApiController
    {
        private RestContext _context;
        private ApplicationUser _user;
        private ILanguageProvider _languageProvider;

        public ChallengeController(RestContext context, ILanguageProvider languageProvider)
        {
            _context = context;
            _languageProvider = languageProvider;
        }

        private ApplicationUser User
        {
            get
            {
                if (_user != null)
                    return _user;
                var username = RequestContext.Principal.Identity.Name;
                var user = _context.Users.FirstOrDefault(u => u.UserName == username

                   );
                _user = user;
                return user;
            }
        }


        /// <summary>
        /// Gets all the challenges the user has done. It can be that the user skipped days.
        /// </summary>
        /// <returns>Challenge data</returns>
        public IEnumerable<object> GetChallenges()
        {
            var acceptLanguages = Request.Headers.AcceptLanguage.FirstOrDefault();
            string language = acceptLanguages == null ? "nl-BE" : acceptLanguages.Value;

            var challenges = User.Challenges;

            challenges.ToList().ForEach(c => _languageProvider.Register(c));

            _languageProvider.Translate(language);
            return challenges.Select(c =>
                new
                {
                    ChallengeId = c.ChallengeId,
                    Date = c.Date,
                    Done = c.Done,
                    Name = c.Name,
                    Earnings = c.Earnings,
                    Type = c.Type
                });
        }

        /// <summary>
        /// Returns all the information about a challenge that a user has.
        /// </summary>
        /// <param name="id">The challengeid</param>
        /// <returns>The requested challenge, with all the information</returns>
        public IHttpActionResult GetChallenge(int id)
        {
            var acceptLanguages = Request.Headers.AcceptLanguage.FirstOrDefault();
            string language = acceptLanguages == null ? "nl-BE" : acceptLanguages.Value;

            var challenge = User.Challenges.FirstOrDefault(c => c.ChallengeId == id);
            if (challenge == null)
                return BadRequest($"Challenge with {id} was not found for this user.");

            _languageProvider.Register(challenge);
            _languageProvider.Translate(language);

            return Ok(challenge);
        }


        /// <summary>
        /// Creates a challenge for the user, according to user preferences.
        /// 
        /// A user can only request one challenge a day.
        /// 
        /// Supported: (tag.)Recipe, CreativeCooking, Restaurant, other tag is converted to text challenge
        /// </summary>
        /// <remarks>A user can only request one challenge a day.</remarks>
        /// <exception cref="ArgumentException">When this function gets called more than once a day.</exception>
        /// <param name="cvm"></param>
        /// <returns>200: Challenge was succesfully added.
        /// 400: An exception has accord. (Message)
        /// </returns>
        [HttpPut]
        public IHttpActionResult InsertChallenge(ChallengeViewModel cvm)
        {
            try
            {

                if (cvm == null || cvm.Type == null)
                {
                    throw new ArgumentException("No data.");
                }
                var challenge = cvm.CreateFactory().CreateChallenge(_context, cvm);
                User.AddChallenge(challenge);

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Set the challenge generated today to be done. The earnings are added to the user points and batches are rewarded.
        /// The challenge must be created today.
        /// </summary>
        /// <param name="id">The challenge that has been done.</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult MarkChallengeAsDone(int id)
        {
            var challenge = User.Challenges.FirstOrDefault(c => c.ChallengeId == id);

            if (challenge == null)
                return BadRequest($"Challenge resource with id {id} does not exist for the current user.");

            try
            {
                User.HasDoneChallenge(challenge);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            _context.SaveChanges();

            return Ok();


        }

        /// <summary>
        /// Delete all the challenges that the users has.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteAllChallenges()
        {
            User.DeleteChallenges();
            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Deletes a challenge that the user has.
        /// </summary>
        /// <param name="id">The id of the challenge.</param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteChallenge(int id)
        {
            var challenge = User.Challenges.FirstOrDefault(c => c.ChallengeId == id);

            if (challenge == null)
                return BadRequest($"Challenge resource with id {id} does not exist for the current user.");

            User.DeleteChallenge(challenge);
            _context.SaveChanges();
            return Ok();
        }
    }
}
