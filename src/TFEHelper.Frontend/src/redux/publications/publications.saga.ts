import { AnyAction } from "redux-saga";
import {call, put, takeLatest} from 'redux-saga/effects'
import { fetchPublications, fetchPublicationsError, fetchPublicationsSuccess } from "./publications.slice";
import { getPublications } from "../../rest-api/publications.api";

export function* fetchPublicationSaga(): Generator<AnyAction, void, any> {
    try{
        yield put(fetchPublications);
        const publications = yield call(getPublications)
        yield put(fetchPublicationsSuccess(publications));
    } catch {
        yield put(fetchPublicationsError('Error fetching publications'));
    }
}


export function* publicationsSaga(){
    yield takeLatest(fetchPublications.type.toString(), fetchPublicationSaga)
}