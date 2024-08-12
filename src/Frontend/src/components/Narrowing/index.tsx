import { Option } from 'antd/es/mentions';
import { ButtonContainer, MainContainer, TagsContainer } from './style';
import { Button, Col, Form, Input, Row, Select, Tag } from 'antd';
import { useState } from 'react';
import Swal from 'sweetalert2';
import { INarrowings } from '../../types/search.types';

interface Props {
  onChange: React.Dispatch<INarrowings[]>;
}

interface IValidationErrorArray {
  selectValue: boolean;
  textInput1: boolean;
  textInput2: boolean;
  distance: boolean;
}

const initailValidationArray = {
  selectValue: false,
  textInput1: false,
  textInput2: false,
  distance: false,
};

export const NarrowingComponent: React.FC<Props> = ({ onChange }) => {
  const [form] = Form.useForm();
  const [selectValue, setSelectValue] = useState<string>('Abstract');
  const [textInput1, setTextInput1] = useState<string>('');
  const [textInput2, setTextInput2] = useState<string>('');
  const [numberDistance, setNumberDistance] = useState<number>(1);
  const [validationArray, setValidationArray] =
    useState<IValidationErrorArray>(initailValidationArray);
  const [narrowingsArray, setNarrowingsArray] = useState<INarrowings[]>([]);

  const handleAddNarrowing = () => {
    // Validation
    if (textInput1 === '' || textInput2 === '') {
      setValidationArray({ ...validationArray, textInput1: true, textInput2: true });

      Swal.fire({
        title: 'Error',
        icon: 'error',
        text: 'Please complete both sentences.',
      });

      return;
    }
    const arr: INarrowings[] = [
      ...narrowingsArray,
      {
        id: Date.now(),
        searchIn: selectValue,
        firstInput: textInput1,
        secondInput: textInput2,
        distance: numberDistance,
      },
    ];
    // Add the narrowing item to array
    setNarrowingsArray(arr);

    // Return to parent component, narrowings array
    onChange(arr);
  };

  const handleOnFormChange = () => {
    setValidationArray(initailValidationArray);
  };

  const handleOnCloseTag = (id: number) => {
    setNarrowingsArray(narrowingsArray.filter(n => n.id !== id));
    onChange(narrowingsArray);
  };

  const handleOnClear = () => {
    // TODO: Improve this form data handling
    form.resetFields();
    setValidationArray(initailValidationArray);
    setNarrowingsArray([]);
    setTextInput1('');
    setTextInput2('');
    setNumberDistance(1);
    onChange([]);
  };

  return (
    <MainContainer>
      <h3>Narrowings</h3>
      <Form form={form} layout="vertical" onChange={handleOnFormChange}>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Search In"
              name="select"
              initialValue={selectValue}
              rules={[{ required: false, message: 'Please select an option!', whitespace: false }]}
            >
              <Select onChange={e => setSelectValue(e)}>
                <Option value="Abstract">Abstract</Option>
                <Option value="Title">Title</Option>
              </Select>
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="First Sentence"
              name="textInput1"
              validateStatus={validationArray.textInput1 ? 'error' : 'success'}
              initialValue={textInput1}
              rules={[{ required: false, message: 'Please input text 1!', whitespace: false }]}
            >
              <Input value={textInput1} onChange={e => setTextInput1(e.target.value)} />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Second Sentence"
              name="textInput2"
              initialValue={textInput2}
              validateStatus={validationArray.textInput2 ? 'error' : 'success'}
              rules={[{ required: false, message: 'Please input text 2!', whitespace: false }]}
            >
              <Input value={textInput2} onChange={e => setTextInput2(e.target.value)} />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Distance number"
              name="numberInput"
              validateStatus={validationArray.distance ? 'error' : 'success'}
              initialValue={numberDistance}
              rules={[{ required: false, message: 'Please input a number!' }]}
            >
              <Input
                type="number"
                onChange={e => setNumberDistance(parseInt(e.target.value))}
                style={{ width: '100%' }}
              />
            </Form.Item>
          </Col>
        </Row>
      </Form>

      <TagsContainer>
        {narrowingsArray.length > 0 ? (
          narrowingsArray.map(n => {
            return (
              <Tag key={n.id} closable onClose={() => handleOnCloseTag(n.id)}>
                {' '}
                {`${n.searchIn}: ${n.firstInput}/${n.secondInput} [${n.distance}]`}
              </Tag>
            );
          })
        ) : (
          <></>
        )}
      </TagsContainer>
      <ButtonContainer>
        <Button onClick={handleAddNarrowing}>Add</Button>
        <Button onClick={handleOnClear}> Clear </Button>
      </ButtonContainer>
    </MainContainer>
  );
};
