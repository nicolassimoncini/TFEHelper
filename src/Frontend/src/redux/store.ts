import createSagaMiddleware from 'redux-saga';
import { Store, combineReducers, configureStore } from '@reduxjs/toolkit';
import rootSaga from './sagas';
import { publicationsReducer } from './publications/publications.slice';
import { configurationReducer } from './configurations/configuration.slice';
import { configurationInitialState } from './configurations/configuration.initial-state';
import { publicationInitialState } from './publications/publication.initial-state';

export const initStore = (): Store => {
    const sagaMiddleware = createSagaMiddleware()
    const middleware = [sagaMiddleware]
    const store: Store = configureStore({
        preloadedState:{
            publication: publicationInitialState,
            configuration: configurationInitialState,
        },
        reducer: combineReducers({
            publication: publicationsReducer,
            configuration: configurationReducer
        }),
        middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(middleware)
    })
    sagaMiddleware.run(rootSaga);
    return store
}