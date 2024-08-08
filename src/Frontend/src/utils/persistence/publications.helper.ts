import { ConfigurationItem } from "../../types/configurations.types";
import { Publication } from "../../types/publications.types";
import { DataType } from "../../types/table.types";

export const mapPluginPublication = (publications: Publication[]): DataType[] => {
    return publications.map(publication => ({
    id: publication.key,
    key: publication.key,
    title: publication.title || '-',
    abstract: publication.abstract || '-',
    authors: publication.authors || '-',
    year: publication.year || null,
    source: (publication.source as ConfigurationItem).name || '-',
    keywords: publication.keywords || '-',
    doi: publication.doi || '-',
    isbn: publication.isbn || '-',
    issn: publication.issn || '-',
    pages: publication.pages || '-',
    url: publication.url || '-'
  }));
}

export const dataTypePlugin2Publication = (data: DataType[], type: number, source: number): Partial<Publication>[] => {

  return data.map( d => ({
    key: d.key || 0,
    title: d.title,
    abstract: d.abstract, 
    authors: d.authors,
    year: d.year || null,
    source: source,
    keywords: d.keywords,
    doi: d.doi, 
    isbn: d.isbn, 
    issn: d.issn,
    pages: d.pages,
    type: type,
    url: d.url || ''
  }))
}

export const mapPublications = (publications: Publication[]): DataType[] => {
  return publications.map(publication => ({
    id: publication.id,
    key: publication.key,
    title: publication.title || '-',
    abstract: publication.abstract || '-',
    authors: publication.authors || '-',
    year: publication.year || null,
    source: (publication.source as ConfigurationItem).name || '-',
    keywords: publication.keywords || '-',
    doi: publication.doi || '-',
    isbn: publication.isbn || '-',
    issn: publication.issn || '-',
    pages: publication.pages || '-',
    url: publication.url || '-'
  }));
};

export const dataType2Publication = (data: DataType[], type: number, source: number): Publication[] => {

  return data.map( d => ({
    id: d.id,
    key: d.key || 0,
    title: d.title,
    abstract: d.abstract, 
    authors: d.authors,
    year: d.year || null,
    source: source,
    keywords: d.keywords,
    doi: d.doi, 
    isbn: d.isbn, 
    issn: d.issn,
    pages: d.pages,
    type: type,
    url: d.url || ''
  }))
}
