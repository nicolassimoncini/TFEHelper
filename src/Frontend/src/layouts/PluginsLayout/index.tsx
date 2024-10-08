import React, { useEffect, useMemo, useState } from 'react';
import { getPlugins } from '../../rest-api/plugins.api';
import { IPlugin } from '../../types/plugin.type';
import {
  ButtonRow,
  Container,
  FormSection,
  PluginContainer,
  PluginSelector,
  SelectorContainer,
} from './style';
import { DropdownComponent, MenuItem } from '../../components/Dropdown';
import { WrapComponent } from '../../components/WrapComponent';
import { PluginForm } from './Form';
import { TableComponent } from '../../components/Table';
import { DataType } from '../../types/table.types';
import { Button } from 'antd';
import { postPublications } from '../../rest-api/publications.api';
import { dataTypePlugin2Publication } from '../../utils/persistence/publications.helper';
import { successAlert } from '../../components/Notifications';
import { useDispatch, useSelector } from 'react-redux';
import { fetchConfiguration } from '../../redux/configurations/configuration.slice';
import { Store } from '../../types/store.types';

interface Props {}

export const PluginsLayout: React.FC<Props> = () => {
  const [publications, setPublications] = useState<DataType[]>([]);
  const [selectedItem, setSelectedItem] = useState<MenuItem | null>(null);
  const [activePlugin, setActivePlugin] = useState<IPlugin | null>(null);
  const [plugins, setPlugins] = useState<IPlugin[]>([]);
  const [isError, setIsError] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [publicationLoader, setPublicationLoader] = useState<boolean>(false);
  const [publicationError, setPublicationError] = useState<boolean>(false);

  const [selectedPubs, setSelectedPubs] = useState<string[]>([]);
  const sourceArr = useSelector((state: Store) => state.configuration.SearchSourceTypeConfig);

  const dispatch = useDispatch();

  // Get plugins
  useEffect(() => {
    dispatch(fetchConfiguration());
    getPlugins()
      .then(res => {
        setPlugins(res);
        setIsLoading(false);
      })
      .catch(e => setIsError(true));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  // Use Memo
  useMemo(() => {
    if (!!selectedItem) {
      const p = plugins.find(p => p.id === parseInt(selectedItem.key));

      if (!!p) setActivePlugin(p);
    }
  }, [selectedItem, plugins]);

  const handleOnSubmit = () => {
    const pubs = publications.filter(p => selectedPubs.includes(p.id as string));
    const source = sourceArr.items.find(s => s.name === pubs[0].source);

    setIsLoading(true);

    postPublications(
      dataTypePlugin2Publication(
        pubs,
        parseInt(selectedItem?.key as string),
        parseInt(source?.value as unknown as string),
      ),
    )
      .then(() => setIsLoading(false))
      .then(() => successAlert('Publications saved correctly!.'))
      .catch(e => setPublicationError(true));
  };

  return (
    <WrapComponent isLoading={isLoading} isError={isError}>
      <Container>
        <h1> Plugins </h1>
        <SelectorContainer>
          <PluginContainer>
            <PluginSelector>
              <p>Plugin</p>
              <DropdownComponent
                options={plugins.map(p => {
                  return {
                    key: `${p.id}`,
                    label: p.name,
                  } as MenuItem;
                })}
                name="Plugin"
                isLoading={isLoading}
                disabled={false}
                selectedOption={selectedItem}
                setSelectedOption={setSelectedItem}
              />
            </PluginSelector>
            <FormSection>
              <PluginForm
                plugin={activePlugin}
                setPublications={setPublications}
                setPublicationLoader={setPublicationLoader}
              ></PluginForm>
            </FormSection>
          </PluginContainer>
        </SelectorContainer>
        <TableComponent
          publications={publications}
          isLoading={publicationLoader}
          isError={publicationError}
          onSelect={pubs => setSelectedPubs(pubs)}
        ></TableComponent>
        {selectedPubs.length !== 0 ? (
          <ButtonRow>
            <Button type="primary" onClick={handleOnSubmit}>
              Save
            </Button>
          </ButtonRow>
        ) : (
          <></>
        )}
      </Container>
    </WrapComponent>
  );
};
