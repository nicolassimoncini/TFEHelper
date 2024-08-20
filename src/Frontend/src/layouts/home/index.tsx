import { useEffect, useState } from 'react';
import { TableComponent } from '../../components/Table';
import { ButtonContainer, HomeLayout, SearchContainer, TableContainer } from './style';
import { useDispatch, useSelector } from 'react-redux';
import { fetchConfiguration } from '../../redux/configurations/configuration.slice';
import { deletePublication, getPublications } from '../../rest-api/publications.api';
import { DataType } from '../../types/table.types';
import { dataType2Publication, mapPublications } from '../../utils/persistence/publications.helper';
import { FilterComponent } from '../filterLayout';
import { Button } from 'antd';
import { SearchOutlined } from '@ant-design/icons';
import { ModalExportPubs } from '../../components/Modal/ExportPubs/index';
import { Store } from '../../types/store.types';

export const HomePage = () => {
  const [publications, setPublications] = useState<DataType[]>([]);
  const [selectedPublicationIds, setSelectedPublicationIds] = useState<string[]>([]);
  const [isError, setIsError] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [openFilter, setOpenFilter] = useState<boolean>(false);
  const [openExportModal, setOpenExportModal] = useState<boolean>(false);
  const [shownNumber, setShownNumber] = useState<number>(0);
  const [total, setTotal] = useState<number>(parseInt(localStorage.getItem('total') || '0'));

  const dispatch = useDispatch();
  const sourceArr = useSelector((state: Store) => state.configuration.SearchSourceTypeConfig);

  const requestPublications = async (): Promise<void> => {
    await getPublications()
      .then(response => {
        setTotal(response.length);
        setPublications(mapPublications(response, sourceArr));
      })
      .catch(() => setIsError(true))
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    dispatch(fetchConfiguration());
    requestPublications();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setShownNumber(publications.length);
  }, [publications]);

  const handleOnDelete = async () => {
    const promises = selectedPublicationIds.map(id => deletePublication(id as string));

    try {
      setIsLoading(true);
      await Promise.all(promises)
        .then(() => requestPublications)
        .finally(() => setIsLoading(false));
    } catch (error) {
      setIsLoading(false);
      setIsError(true);
    }

    window.location.reload();
  };

  const handleOnExport = async () => {
    setOpenExportModal(true);
  };

  return (
    <HomeLayout>
      <>
        <SearchContainer>
          <Button type="primary" icon={<SearchOutlined />} onClick={() => setOpenFilter(true)}>
            Filter
          </Button>
          <p>Result: {`${shownNumber}/${total}`}</p>
        </SearchContainer>
        <FilterComponent
          open={openFilter}
          setPublications={value => {
            setPublications(value);
            setShownNumber(value.length);
          }}
          setOpen={setOpenFilter}
        ></FilterComponent>
        <TableContainer>
          <TableComponent
            publications={publications}
            isError={isError}
            isLoading={isLoading}
            onSelect={pubs => setSelectedPublicationIds(pubs)}
          ></TableComponent>
        </TableContainer>
        <ButtonContainer>
          {shownNumber !== total ? (
            <Button type="primary" onClick={() => requestPublications()}>
              Clear
            </Button>
          ) : (
            <></>
          )}
          {selectedPublicationIds.length > 0 ? (
            <>
              <Button type="primary" onClick={handleOnExport}>
                {' '}
                Export{' '}
              </Button>

              <Button danger={true} onClick={handleOnDelete}>
                {' '}
                Delete{' '}
              </Button>
            </>
          ) : (
            <></>
          )}
        </ButtonContainer>
      </>
      <ModalExportPubs
        isOpen={openExportModal}
        setIsOpen={value => setOpenExportModal(value)}
        pubs={dataType2Publication(
          publications.filter(p => selectedPublicationIds.includes(p.id as string)),
        )}
      ></ModalExportPubs>
    </HomeLayout>
  );
};
