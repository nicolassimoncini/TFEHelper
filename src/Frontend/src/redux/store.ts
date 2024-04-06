import createSagaMiddleware from 'redux-saga';
import { combineReducers, configureStore, Store } from '@reduxjs/toolkit';
import rootSaga from './sagas';
import { publicationsReducer } from './publications/publications.slice';

export const initStore = (): Store => {
    const sagaMiddleware = createSagaMiddleware()
    const middleware = [sagaMiddleware]
    const store: Store = configureStore({
        reducer: combineReducers([
            publicationsReducer,
        ]),
        middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(middleware)
    })
    sagaMiddleware.run(rootSaga)
    return store
}