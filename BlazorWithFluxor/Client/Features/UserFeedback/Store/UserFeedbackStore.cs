using BlazorWithFluxor.Shared;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWithFluxor.Client.Features.UserFeedback.Store
{
    public record UserFeedbackState
    {
        public bool Submitting { get; init; }
        public bool Submitted { get; init; }
        public string ErrorMessage { get; init; }
        public UserFeedbackModel Model { get; init; }
    }

    public class UserFeedbackFeature : Feature<UserFeedbackState>
    {
        public override string GetName() => "UserFeedback";

        protected override UserFeedbackState GetInitialState()
        {
            return new UserFeedbackState
            {
                Submitting = false,
                Submitted = false,
                ErrorMessage = string.Empty,
                Model = new UserFeedbackModel()
            };
        }
    }

    // Actions
    public class UserFeedbackSubmitSuccessAction { }

    public class UserFeedbackSubmitFailureAction
    {
        public string ErrorMessage { get; }
        public UserFeedbackSubmitFailureAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    public class UserFeedbackSubmitAction
    {
        public UserFeedbackModel UserFeedbackModel { get; }

        public UserFeedbackSubmitAction(UserFeedbackModel userFeedbackModel)
        {
            UserFeedbackModel = userFeedbackModel;
        }
    }

    // Effect
    public class UserFeedbackEffects
    {
        private readonly HttpClient _httpClient;
        public UserFeedbackEffects(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [EffectMethod]
        public async Task SubmitUserFeedback(UserFeedbackSubmitAction action, IDispatcher dispatcher)
        {
            var response = await _httpClient.PostAsJsonAsync("Feedback", action.UserFeedbackModel);

            if (response.IsSuccessStatusCode)
            {
                dispatcher.Dispatch(new UserFeedbackSubmitSuccessAction());
            }
            else
            {
                dispatcher.Dispatch(new UserFeedbackSubmitFailureAction(response.ReasonPhrase));
            }
        }
    }

    public static class UserFeedbackReducers
    {
        [ReducerMethod(typeof(UserFeedbackSubmitAction))]
        public static UserFeedbackState OnSubmit(UserFeedbackState state)
        {
            return state with
            {
                Submitting = true
            };
        }

        [ReducerMethod(typeof(UserFeedbackSubmitSuccessAction))]
        public static UserFeedbackState OnSubmitSuccess(UserFeedbackState state)
        {
            return state with
            {
                Submitting = false,
                Submitted = true
            };
        }

        [ReducerMethod]
        public static UserFeedbackState OnSubmitFailure(UserFeedbackState state, UserFeedbackSubmitFailureAction action)
        {
            return state with
            {
                Submitting = false,
                ErrorMessage = action.ErrorMessage
            };
        }
    }
}
