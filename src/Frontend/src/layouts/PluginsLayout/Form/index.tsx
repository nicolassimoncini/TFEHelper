import TextArea from 'antd/es/input/TextArea';
import { IPlugin } from '../../../types/plugin.type';
import { useEffect, useState } from 'react';
import { DropdownComponent, MenuItem } from '../../../components/Dropdown';
import {
  ButtonsContainer,
  Container,
  DateSelectorContainter,
  QuantitySelectorContainer,
  QueryFieldContainer,
  SubjectSelectorContainer,
} from './style';
import { Button, DatePicker } from 'antd';
import Input from 'antd/es/input/Input';
import { DataType } from '../../../types/table.types';

interface Props {
  plugin: IPlugin | undefined;
  setPublications: React.Dispatch<React.SetStateAction<DataType[]>>;
}

export const PluginForm: React.FC<Props> = ({ plugin, setPublications }) => {
  const [searchString, setSearchString] = useState<string>('');
  const [searchDate, setSearchDate] = useState<(string | null)[]>([]);
  const [pNumber, setPNumber] = useState<number>(10);
  const [selectedSubject, setSelectedSubject] = useState<MenuItem | null>(null);
  const [menuItems, setMenuItems] = useState<MenuItem[]>([]);

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

  const handleOnSubmit = () => {
    // TODO: Handle request
    // TODO: Add field validations
  };

  return (
    <Container>
      <SubjectSelectorContainer>
        <p>Select Subject</p>
        <DropdownComponent
          options={menuItems}
          name="Subject"
          isLoading={false}
          selectedOption={selectedSubject}
          setSelectedOption={setSelectedSubject}
        />
      </SubjectSelectorContainer>
      <QueryFieldContainer>
        <p>Search string</p>
        <TextArea rows={4} value={searchString} onChange={e => setSearchString(e.target.value)} />
      </QueryFieldContainer>
      <DateSelectorContainter>
        <p>Date selector</p>
        <DatePicker.RangePicker picker="year" onChange={(_, e) => setSearchDate(e)} />
      </DateSelectorContainter>
      <QuantitySelectorContainer>
        <p>Number of articles</p>
        <Input
          type="number"
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
        <Button> Clear </Button>
      </ButtonsContainer>
    </Container>
  );
};
