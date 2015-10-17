using EVARest.Models.DAL;
using EVARest.Models.Domain;
using EVARest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EVARest.Controllers {
    /// <summary>
    /// The challenge resource.
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Challenges")]
    public class ChallengeController : ApiController {
        private RestContext _context;
        private ApplicationUser _user;
        private ApplicationUser User {
            get {
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
        public IEnumerable<object> GetChallenges() {
            return User.Challenges.Select(c =>
new {
    ChallengeId = c.ChallengeId,
    Date = c.Date,
    Done = c.Done,
    Name = c.Name,
    Earnings = c.Earnings


});
        }

        /// <summary>
        /// Returns all the information about a challenge that a user has.
        /// </summary>
        /// <param name="id">The challengeid</param>
        /// <returns>The requested challenge, with all the information</returns>
        public IHttpActionResult GetChallenge(int id) {
            var challenge = User.Challenges.FirstOrDefault(c => c.ChallengeId == id);
            if (challenge == null)
                return BadRequest($"Challenge with {id} was not found for this user.");

            return Ok(challenge);
        }


        /// <summary>
        /// Creates a challenge for the user, according to user preferences.
        /// 
        /// A user can only request one challenge a day.
        /// </summary>
        /// <remarks>A user can only request one challenge a day.</remarks>
        /// <exception cref="ArgumentException">When this function gets called more than once a day.</exception>
        /// <param name="cvm"></param>
        /// <returns>200: Challenge was succesfully added.
        /// 400: An exception has accord. (Message)
        /// </returns>
        [HttpPut]
        public IHttpActionResult InsertChallenge(ChallengeViewModel cvm) {
            try {

                if (cvm == null || cvm.Type == null) {
                    throw new ArgumentException("No data.");
                }
                var challenge = cvm.CreateFactory().CreateChallenge(_context, cvm);
                User.AddChallenge(challenge);
          
                _context.SaveChanges();
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

 
        


        public ChallengeController(RestContext context) {
            _context = context;
        }


    }
}
