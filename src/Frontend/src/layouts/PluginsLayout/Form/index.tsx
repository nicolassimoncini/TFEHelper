import TextArea from 'antd/es/input/TextArea';
import { Container } from '../style';
import { IPlugin } from '../../../types/plugin.type';
import { useEffect, useState } from 'react';
import { DropdownComponent, MenuItem } from '../../../components/Dropdown';
import {
  DateSelectorContainter,
  QuantitySelectorContainer,
  QueryFieldContainer,
  SubjectSelectorContainer,
} from './style';
import { DatePicker } from 'antd';
import Input from 'antd/es/input/Input';

interface Props {
  plugin: IPlugin | undefined;
}

export const PluginForm: React.FC<Props> = ({ plugin }) => {
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

  return (
    <Container>
      <QueryFieldContainer>
        <p>Search string</p>
        <TextArea rows={4} />
      </QueryFieldContainer>
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
      <DateSelectorContainter>
        <p>Date selector</p>
        <DatePicker.RangePicker picker="year"></DatePicker.RangePicker>
      </DateSelectorContainter>
      <QuantitySelectorContainer>
        <p>Number of articles</p>
        <Input type="number"></Input>
      </QuantitySelectorContainer>
    </Container>
  );
};
