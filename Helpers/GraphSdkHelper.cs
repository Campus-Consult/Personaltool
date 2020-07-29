using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Personaltool.Models;

namespace Personaltool.Helpers {
    public class GraphSdkHelper {

        /// Utility method to create an authenticated Graph client
        /// takes AuthenticationProperties as an argument, which can be
        /// aquired with `(await HttpContext.AuthenticateAsync()).Properties`
        public static IGraphServiceClient GetAuthenticatedClient(AuthenticationProperties props) {
            var accessToken = props.GetTokenValue("access_token");
            if (accessToken == null) {
                throw new ArgumentException("access_token in AuthenticationProperties can't be null!");
            }
            return new GraphServiceClient(new DelegateAuthenticationProvider(
                requestMessage =>
                {
                    // Append the access token to the request
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    return Task.CompletedTask;
                }));
        }
    }
}