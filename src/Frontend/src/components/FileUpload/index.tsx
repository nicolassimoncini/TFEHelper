import { InboxOutlined } from '@ant-design/icons';
import { Upload, UploadProps, message } from 'antd';
import { RcFile } from 'antd/es/upload';
import React, { useState } from 'react';
import { UploadContainer } from './style';

const { Dragger } = Upload;

interface FileUploadDraggerProps {
  onFileUpload: (file: File) => void;
}

// Define a function to map RcFile to File
const mapRcFileToFile = (rcFile: RcFile): File => {
  const file: File = rcFile as File;
  return file;
};

const FileUploadComponent: React.FC<FileUploadDraggerProps> = ({ onFileUpload }) => {
  const [fileList, setFileList] = useState<RcFile[]>([]);

  const props: UploadProps = {
    name: 'file',
    multiple: false,
    fileList,
    onChange(info) {
      setFileList(info.fileList.map(file => file.originFileObj as RcFile));
    },
    beforeUpload(file) {
      const fileExtension = file.name.split('.').pop()?.toLowerCase();
      if (fileExtension !== 'bib' && fileExtension !== 'csv') {
        message.error('Please upload a BibTeX (.bib) or CSV (.csv) file only.');
        return false; // Prevent file upload
      }
      onFileUpload(mapRcFileToFile(file) as File); // Call the callback function to pass the uploaded file to the parent component
      return false; // Prevent default upload behavior
    },
  };

  return (
    <UploadContainer>
      <Dragger {...props}>
        <p className="ant-upload-drag-icon">
          <InboxOutlined />
        </p>
        <p className="ant-upload-text">Click or drag BibTeX (.bib) or CSV (.csv) file to upload</p>
        <p className="ant-upload-hint"></p>
      </Dragger>
    </UploadContainer>
  );
};

export default FileUploadComponent;
