import { AnyAction } from "redux-saga";
import {call, put, takeEvery} from 'redux-saga/effects'
import { fetchPublications, fetchPublicationsError, fetchPublicationsSuccess, uploadFile, uploadFileSuccess } from "./publications.slice";
import { getPublications, uploadFileRequest } from "../../rest-api/publications.api";

export function* fetchPublicationSaga(): Generator<AnyAction, void, any> {
    try{
        yield put(fetchPublications);
        const publications = yield call(getPublications);
        yield put(fetchPublicationsSuccess(publications));
    } catch {
        yield put(fetchPublicationsError('Error fetching publications'));
    }
}

export function* uploadFileSaga(action: AnyAction): Generator<AnyAction, void, any> {
    try {
        yield put(uploadFile());
        yield call(uploadFileRequest, action.payload);
        yield put(uploadFileSuccess());
    } catch (error) {
        yield put(fetchPublicationsError('Error uploading file'));
    }
}


export function* publicationsSaga(){
    yield takeEvery(fetchPublications.type.toString(), fetchPublicationSaga)
    yield takeEvery(uploadFile.type.toString(), uploadFileSaga)
}