import { Publication } from "../../types/publications.types";
import { DataType } from "../../types/table.types";

export const mapPublications = (publications: Publication[]): DataType[] => {
  return publications.map(publication => ({
    key: publication.id,
    title: publication.title || '-',
    abstract: publication.abstract || '-',
    authors: publication.authors || '-',
    year: publication.year || '-',
    source: publication.source.name || '-',
    keywords: publication.keywords || '-',
    doi: publication.doi || '-',
    isbn: publication.isbn || '-',
    issn: publication.issn || '-',
    pages: publication.pages || '-',
  }));
};
