import { all } from "redux-saga/effects";
import { publicationsSaga } from "./publications/publications.saga";
import { configurationSaga } from "./configurations/configuration.saga";

export default function* rootSaga() {
    yield all([publicationsSaga, configurationSaga])
}