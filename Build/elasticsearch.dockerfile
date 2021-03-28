FROM docker.elastic.co/elasticsearch/elasticsearch:7.11.2 AS elasticsearch

WORKDIR /usr/share/elasticsearch/config/hunspell/ru_RU
COPY ./hunspell/ru_RU ./

WORKDIR /usr/share/elasticsearch/config/synonyms/ru_RU
COPY ./synonyms/ru_RU ./