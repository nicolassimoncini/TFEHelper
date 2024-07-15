import React, { useEffect, useMemo, useState } from 'react';
import { getPlugins } from '../../rest-api/plugins.api';
import { IPlugin } from '../../types/plugin.type';
import {
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

  // Get plugins
  useEffect(() => {
    getPlugins()
      .then(res => {
        setPlugins(res);
        setIsLoading(false);
      })
      .catch(e => setIsError(true));
  }, []);

  // Use Memo
  useMemo(() => {
    if (!!selectedItem) {
      const p = plugins.find(p => p.id === parseInt(selectedItem.key));

      if (!!p) setActivePlugin(p);
    }
  }, [selectedItem, plugins]);

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
        ></TableComponent>
      </Container>
    </WrapComponent>
  );
};
