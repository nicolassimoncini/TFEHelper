import { Button, Divider } from 'antd';
import FileUploadDragger from '../../components/FileUpload';
import {
  ButtonsContainer,
  FileFormatContainer,
  ImportFormDataLayout,
  ImportLayout,
  ImportLayoutDragger,
  SourceContainer,
} from './style';
import { DropdownComponent, MenuItem } from '../../components/Dropdown';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { selectConfigurations } from '../../redux/configurations/selectors';
import { fetchConfiguration } from '../../redux/configurations/configuration.slice';
import Swal from 'sweetalert2';
import { uploadFileRequest } from '../../rest-api/publications.api';
import { redirect } from 'react-router-dom';
import Loader from '../../components/Loader';

export const ImportFileLayout: React.FC = () => {
  const [selectedFileFormat, setSelectedFileFormat] = useState<MenuItem | null>(null);
  const [selectedSource, setSelectedSource] = useState<MenuItem | null>(null);
  const [file, setFile] = useState<File>();

  const configSelector = useSelector(selectConfigurations);
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchConfiguration());

    return () => {};
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleFileUpload = (inputFile: File) => {
    setFile(inputFile);
  };

  const handleImportOnClick = async () => {
    // Validate file and metadata
    if (!selectedFileFormat || !selectedSource || !file) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Please select a file format, source, and a file to import.',
      });

      console.log(file, selectedFileFormat, selectedSource);
    } else {
      const response = await uploadFileRequest({
        file: file,
        formatType: Number(selectedFileFormat.key),
        source: Number(selectedSource.key),
        discardInvalidRecords: true,
      });
      if (response.status === 200) {
        Swal.fire({
          icon: 'success',
          title: 'Success!',
          text: 'File has been imported successfully.',
        });
        return redirect('/');
      } else {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Error importing file.',
        });
      }
    }

    console.log('Importing file...');
  };

  return (
    <>
      {configSelector.isLoading ? (
        <Loader />
      ) : (
        <ImportLayout>
          <Divider orientation="left"> File metadata </Divider>
          <ImportFormDataLayout>
            <FileFormatContainer>
              <p>File Format</p>
              <DropdownComponent
                options={configSelector.FileFormatTypeConfig.items.map(
                  item =>
                    ({
                      key: item.value.toString(),
                      label: item.name,
                    }) as MenuItem,
                )}
                name={'File Format'}
                isLoading={configSelector.isLoading}
                selectedOption={selectedFileFormat}
                setSelectedOption={setSelectedFileFormat}
              />
            </FileFormatContainer>
            <SourceContainer>
              <p> Source data </p>
              <DropdownComponent
                options={configSelector.SearchSourceTypeConfig.items.map(
                  item =>
                    ({
                      key: item.value.toString(),
                      label: item.name,
                    }) as MenuItem,
                )}
                name={' Source'}
                isLoading={configSelector.isLoading}
                selectedOption={selectedSource}
                setSelectedOption={setSelectedSource}
              />
            </SourceContainer>
          </ImportFormDataLayout>
          <Divider orientation="left">Select a file</Divider>
          <ImportLayoutDragger>
            <FileUploadDragger onFileUpload={handleFileUpload} />
          </ImportLayoutDragger>
          <ButtonsContainer>
            <Button type="default">Cancel</Button>
            <Button type="primary" onClick={handleImportOnClick}>
              Import
            </Button>
          </ButtonsContainer>
        </ImportLayout>
      )}
    </>
  );
};
