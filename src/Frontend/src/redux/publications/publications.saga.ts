import { AnyAction } from "redux-saga";
import {call, put, takeEvery} from 'redux-saga/effects'
import { fetchPublications, fetchPublicationsError, fetchPublicationsSuccess } from "./publications.slice";
import { getPublications } from "../../rest-api/publications.api";

export function* fetchPublicationSaga(): Generator<AnyAction, void, any> {
    try{
        yield put(fetchPublications);
        const publications = yield call(getPublications);
        yield put(fetchPublicationsSuccess(publications));
    } catch {
        yield put(fetchPublicationsError('Error fetching publications'));
    }
}


export function* publicationsSaga(){
    console.log('Publications saga')
    yield takeEvery(fetchPublications.type.toString(), fetchPublicationSaga)
}