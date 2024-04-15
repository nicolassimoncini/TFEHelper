import { AnyAction } from "redux-saga";
import { call, put, takeEvery } from "redux-saga/effects";
import { getConfigurations } from "../../rest-api/configuration.api";
import { fetchConfiguration, fetchConfigurationError, fetchConfigurationSuccess } from "./configuration.slice";
import { ConfigurationType } from '../../types/configurations.types';

export function* fetchConfigurationsSaga(): Generator<AnyAction, void, any> {
    try {
        // yield put(fetchConfiguration());

        // make the api call to rest api server
        const configurations = yield call(getConfigurations);

        // check if any configuration object is empty
        const isEmpty = configurations.some((cfg: ConfigurationType) => cfg.items.length === 0 )

        // If any configuration object is empty, dispatch action to store
        if (isEmpty){
            yield put(fetchConfigurationError('Some configuration object is empty!'))
        }

        // Dispatch action to populate store
        yield put(fetchConfigurationSuccess({
            configurations: {
                BibTexConfig: configurations.find((cfg: ConfigurationType) => cfg.name === 'BibTeXPublicationDTOType'),
                FileFormatTypeConfig: configurations.find((cfg: ConfigurationType) => cfg.name === 'FileFormatDTOType'),
                SearchSourceTypeConfig: configurations.find((cfg: ConfigurationType) => cfg.name === 'SearchSourceDTOType')
            }
        }))

    } catch (error) {
        console.error('[CONFIG] Error fetching configurations: ', error)
        yield put(fetchConfigurationError('Error fetching confiugurations'))
    }
}

export function* configurationSaga(){
    yield takeEvery(fetchConfiguration.type.toString(), fetchConfigurationsSaga)
}