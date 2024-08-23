# TFEHelper.Frontend

## Overview

TFEHelper Frontend App is a frontend application built using [React](https://reactjs.org/), [Styled Components](https://styled-components.com/), [Ant Design (antd)](https://ant.design/), and [Redux-Saga](https://redux-saga.js.org/) for state management. It is a tool that allows the use of plugins defined in the backend, enabling the visualization, filtering, organization, import, and export of data stored in the database.

## Table of Contents

- [Project Name](#project-name)
  - [Overview](#overview)
  - [Table of Contents](#table-of-contents)
  - [Features](#features)
  - [Installation](#installation)
    - [Environment variables](#environment-variables)
    - [Prerequisites](#prerequisites)
  - [Usage](#usage)
    - [Running the Application](#running-the-application)
    - [Building for Production](#building-for-production)
  - [Folder Structure](#folder-structure)
  - [State Management](#state-management)
  - [Styling](#styling)
  - [Contributing](#contributing)

## Features
- Data Display: Provides an intuitive interface for visualizing and interacting with database data, making it easier to analyze and manage.
- Custom Search Query Builder: Utilizes [React-Query-Builder](https://react-querybuilder.js.org/) to generate search queries, allowing users to filter and retrieve data precisely according to their needs.
- Plugin Selection and Publication Search: Offers an interface for selecting plugins and searching through publications.
- Data Import and Export: Facilitates data import and export, supporting CSV and BibText formats.

## Installation
Using npm:
```bash
npm install
```
Or using yarn:
```bash
yarn install 
```

### Environment variables
Create a .env file in the root directory and add your environment variables as needed. The required environment variables are defined in .env.example file.

### Prerequisites

Before you begin, ensure you have met the following requirements:
- Node.js (version 20.12.2 or higher)
- npm or yarn

## Usage
### Running the Application
To start the development server:
```bash
npm start
```
Or using yarn:
```bash
yarn start
```

### Building for Production
To create a production build:
```bash
npm run build
```
Or using yarn:
```bash
yarn build
```

## Folder Structure
The project structure is as follows:
```
└── - src
    └── - components
    └── - helpers
    └── - layouts
    └── - redux
    └── - rest-api
    └── - routes
    └── - styles
    └── - types
    └── - utils
```
- /components: Contains reusable React components that can be used across different parts of the application.
- /helpers: Includes utility functions and helper methods that assist with specific tasks or computations across the application.
- /layouts: Contains layout components that define the structure and organization of the UI.
- /redux: Houses all files related to state management using Redux. This includes actions, reducers, and store configurations.
- /rest-api: Contains files related to API interaction.
- /routes: Manages the routing of the application.
- /styles: Holds styling-related files, including global styles, theme definitions, and styled-components.
- /types: Stores TypeScript type definitions, interfaces, and enums.
- /utils: Includes utility functions and general-purpose helpers.

## State Management
The application uses [Redux-Saga](https://redux-saga.js.org/) for handling side effects in the application. All the async operations, like API calls, are managed through sagas.
- Sagas are located in src/store/sagas/.
- Reducers are in src/store/reducers/.
- Actions are in src/store/actions/.

## Styling
Styling is handled using [Styled Components](https://styled-components.com/). Global styles and theming are configured in src/styles/.

Example of a styled component:
```bash
import styled from 'styled-components';
export const TableLayout = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 100%;
  scroll-behavior: auto;
`;
```


## Contributing
If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are warmly welcome.

1. Fork the Project
2. Create your Feature Branch (git checkout -b feature/FeatureName)
3. Commit your Changes (git commit -m 'Add some FeatureName')
4. Push to the Branch (git push origin feature/FeatureName)
5. Open a Pull Request

