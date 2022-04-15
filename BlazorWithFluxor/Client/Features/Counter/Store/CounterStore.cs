﻿using Blazored.LocalStorage;
using Fluxor;

namespace BlazorWithFluxor.Client.Features.Counter.Store
{
    public record CounterState
    {
        public int CurrentCount { get; init; }
    }

    public class CounterFeature : Feature<CounterState>
    {
        public override string GetName() => "Counter";

        protected override CounterState GetInitialState()
        {
            return new CounterState
            {
                CurrentCount = 0
            };
        }
    }


    public static class CounterReducers
    {
        [ReducerMethod(typeof(CounterIncrementAction))]
        public static CounterState OnIncrement(CounterState state)
        {
            return state with
            {
                CurrentCount = state.CurrentCount + 1
            };
        }

        [ReducerMethod]
        public static CounterState OnCounterSetState(CounterState state, CounterSetStateAction action)
        {
            return action.CounterState;
        }
    }

    // Effects
    public class CounterEffects
    {
        private readonly ILocalStorageService _localStorageService;
        private const string CounterStatePersistenceName = "BlazorWithFluxor_CounterState";

        public CounterEffects(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        [EffectMethod]
        public async Task PersistState(CounterPersistStateAction action, IDispatcher dispatcher)
        {
            try
            {
                await _localStorageService.SetItemAsync(CounterStatePersistenceName, action.CounterState);
                dispatcher.Dispatch(new CounterPersistStateSuccessAction());
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new CounterPersistStateFailureAction(ex.Message));
            }
        }

        [EffectMethod(typeof(CounterLoadStateAction))]
        public async Task LoadState(IDispatcher dispatcher)
        {
            try
            {
                var counterState = await _localStorageService.GetItemAsync<CounterState>(CounterStatePersistenceName);
                if (counterState is not null)
                {
                    dispatcher.Dispatch(new CounterSetStateAction(counterState));
                    dispatcher.Dispatch(new CounterLoadStateSuccessAction());
                }
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new CounterLoadStateFailureAction(ex.Message));
            }
        }

        [EffectMethod(typeof(CounterClearStateAction))]
        public async Task ClearState(IDispatcher dispatcher)
        {
            try
            {
                await _localStorageService.RemoveItemAsync(CounterStatePersistenceName);
                dispatcher.Dispatch(new CounterSetStateAction(new CounterState { CurrentCount = 0 }));
                dispatcher.Dispatch(new CounterClearStateSuccessAction());
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new CounterClearStateFailureAction(ex.Message));
            }
        }
    }

    #region CounterActions
    public record CounterIncrementAction();
    public record CounterSetStateAction(CounterState CounterState);
    public record CounterPersistStateAction(CounterState CounterState);
    public record CounterPersistStateSuccessAction();
    public record CounterPersistStateFailureAction(string ErrorMessage);
    public record CounterLoadStateAction();
    public record CounterLoadStateSuccessAction();
    public record CounterLoadStateFailureAction(string ErrorMessage);
    public record CounterClearStateAction();
    public record CounterClearStateSuccessAction();
    public record CounterClearStateFailureAction(string ErrorMessage);
    #endregion
}