import { Button, Modal, Radio, RadioChangeEvent } from 'antd';
import { Publication } from '../../../types/publications.types';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import { Store } from '../../../types/store.types';
import { CheckBoxContainer, MainContainer } from './style';
import { exportPublicationsAsStream } from '../../../rest-api/publications.api';

interface Props {
  isOpen: boolean;
  setIsOpen: React.Dispatch<boolean>;
  pubs: Publication[];
}

export const ModalExportPubs: React.FC<Props> = ({ isOpen, pubs, setIsOpen }) => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [value, setValue] = useState<number>(1);

  // Configuration list
  const fileFormats = useSelector((state: Store) => state.configuration.FileFormatTypeConfig);

  const options = fileFormats.items.map(f => ({
    label: f.name,
    value: f.value,
  }));

  const handleOnRadioChange = (e: RadioChangeEvent) => {
    setValue(e.target.value);
  };
  const handleOnExport = async () => {
    setIsLoading(true);
    try {
      await exportPublicationsAsStream(
        options.find(opt => opt.value === value)?.value as number,
        pubs,
      );
    } catch (error) {
      console.log(`Failed to download file`, error);
    } finally {
      setIsLoading(false);
      setIsOpen(false);
    }
  };

  const handleOnClose = () => {
    setIsOpen(false);
  };

  return (
    <Modal
      title={'Export publications'}
      open={isOpen}
      footer={[
        <>
          <Button key={'submit'} type="primary" loading={isLoading} onClick={handleOnExport}>
            Export
          </Button>
          <Button key={'cancel'} type="default" onClick={handleOnClose}>
            Cancel
          </Button>
        </>,
      ]}
    >
      <MainContainer>
        <CheckBoxContainer>
          <Radio.Group options={options} value={value} onChange={handleOnRadioChange} />
        </CheckBoxContainer>
      </MainContainer>
    </Modal>
  );
};
