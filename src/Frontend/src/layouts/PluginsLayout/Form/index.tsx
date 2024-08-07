import TextArea from 'antd/es/input/TextArea';
import { IPlugin } from '../../../types/plugin.type';
import { useEffect, useState } from 'react';
import { DropdownComponent, MenuItem } from '../../../components/Dropdown';
import {
  ButtonsContainer,
  Container,
  DateSelectorContainer,
  NarrowingSelectorContainer,
  QuantitySelectorContainer,
  QueryFieldContainer,
  SubjectSelectorContainer,
} from './style';
import { Button, DatePicker } from 'antd';
import Input from 'antd/es/input/Input';
import { DataType } from '../../../types/table.types';
import { PluginCollectorQuery } from '../../../types/search.types';
import { searchInPlugins } from '../../../rest-api/plugins.api';
import { mapPluginPublication } from '../../../utils/persistence/publications.helper';
import Swal from 'sweetalert2';

interface Props {
  plugin: IPlugin | null;
  setPublications: React.Dispatch<React.SetStateAction<DataType[]>>;
  setPublicationLoader: React.Dispatch<React.SetStateAction<boolean>>;
}

const errorsInit = {
  searchString: false,
  searchDate: false,
  pNumber: false,
};

export const PluginForm: React.FC<Props> = ({ plugin, setPublications, setPublicationLoader }) => {
  const [searchString, setSearchString] = useState<string>('');
  const [searchDate, setSearchDate] = useState<string[]>([]);
  const [narrowing, setNarrowing] = useState<string>('');
  const [pNumber, setPNumber] = useState<number>(10);
  const [selectedSubject, setSelectedSubject] = useState<MenuItem | null>(null);
  const [menuItems, setMenuItems] = useState<MenuItem[]>([]);
  const [errors, setErrors] = useState(errorsInit);

  useEffect(() => {
    if (!!plugin?.parameters?.collectionValued[0].value) {
      const subjects: MenuItem[] = [];
      plugin.parameters.collectionValued[0].value.map(item => {
        if (!subjects.find(s => s.key === item.value))
          subjects.push({
            key: item.value,
            label: item.value,
          });

        return 0;
      });

      setMenuItems(subjects);
    }
  }, [plugin]);

  const handleOnClear = () => {
    setSearchString('');
    setSearchDate([]);
    setNarrowing('');
    setPNumber(10);
  };

  const handleOnSubmit = async () => {
    setErrors(errorsInit);

    // Validate fields
    if (searchString === '') {
      setErrors({ ...errors, searchString: true });
      return;
    }
    if (!searchDate[0] || !searchDate[1]) {
      setErrors({ ...errors, searchDate: true });
      return;
    }

    const queryParams: PluginCollectorQuery = {
      query: searchString,
      searchIn: narrowing,
      subject: selectedSubject?.label || '',
      dateFrom: `${searchDate[0]}-01-01`,
      dateTo: `${searchDate[1]}-12-31`,
      returnQuantityLimit: pNumber,
    };

    setPublicationLoader(true);
    searchInPlugins(plugin!.id.toString(), queryParams)
      .then(res => setPublications(mapPluginPublication(res)))
      .catch(err => {
        console.error(err);
        Swal.fire({
          icon: 'error',
          title: 'Oopss...',
          text: 'Errro while fetching publications',
        });
      })
      .finally(() => setPublicationLoader(false));
  };

  return (
    <Container>
      <SubjectSelectorContainer>
        <p>Select Subject</p>
        <DropdownComponent
          options={menuItems}
          name="Subject"
          disabled={!plugin}
          isLoading={false}
          selectedOption={selectedSubject}
          setSelectedOption={setSelectedSubject}
        />
      </SubjectSelectorContainer>
      <QueryFieldContainer>
        <p>Search string</p>
        <TextArea
          rows={4}
          value={searchString}
          disabled={!plugin}
          onChange={e => setSearchString(e.target.value)}
        />
      </QueryFieldContainer>
      <NarrowingSelectorContainer>
        <p>Search in</p>
        <Input type="text" disabled={!plugin} onChange={e => setNarrowing(e.target.value)} />
      </NarrowingSelectorContainer>
      <DateSelectorContainer>
        <p>Date selector</p>
        <DatePicker.RangePicker
          picker="year"
          allowEmpty={[true, true]}
          disabled={!plugin}
          status={errors.searchDate ? 'error' : ''}
          // value={[dayjs(searchDate[0] || null), dayjs(searchDate[1] || null)]}
          onChange={(_, datestring) => setSearchDate(datestring)}
        />
      </DateSelectorContainer>
      <QuantitySelectorContainer>
        <p>Number of articles</p>
        <Input
          type="number"
          disabled={!plugin}
          min={10}
          max={1000}
          defaultValue={10}
          onChange={e => setPNumber(parseInt(e.target.value))}
        />
      </QuantitySelectorContainer>
      <ButtonsContainer>
        <Button type="primary" onClick={handleOnSubmit}>
          Search
        </Button>
        <Button onClick={handleOnClear}> Clear </Button>
      </ButtonsContainer>
    </Container>
  );
};
