import React from 'react';
import { Upload } from 'antd';
import { InboxOutlined } from '@ant-design/icons';
import Swal from 'sweetalert2';
import { DraggerContainer } from './style';
import { UploadChangeParam } from 'antd/es/upload';

const { Dragger } = Upload;

interface FileUploadDraggerProps {
  onFileUpload: (file: File) => void;
}

const FileUploadDragger: React.FC<FileUploadDraggerProps> = ({ onFileUpload }) => {
  //TODO: Implement the file upload logic for multiple files

  const onChange = (info: UploadChangeParam) => {
    onFileUpload(info.file.originFileObj as File);
  };

  const handleBeforeUpload = (file: File, _: File[]) => {
    const fileExtension = file.name.split('.').pop()?.toLowerCase();
    if (!(fileExtension === 'bib' || fileExtension === 'csv')) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Please upload a CSV or BibTex file.',
      });

      return Upload.LIST_IGNORE;
    }
  };

  const customRequest = ({ file, onSuccess }: any) => {
    if (file instanceof File && file.size > 0) {
      onSuccess('ok');
    }
  };

  return (
    <DraggerContainer>
      <Dragger
        customRequest={customRequest}
        beforeUpload={handleBeforeUpload}
        onChange={onChange}
        multiple={false}
        maxCount={1}
      >
        <p className="ant-upload-drag-icon">
          <InboxOutlined />
        </p>
        <p className="ant-upload-text">Click or drag file to this area to upload</p>
      </Dragger>
    </DraggerContainer>
  );
};

export default FileUploadDragger;
