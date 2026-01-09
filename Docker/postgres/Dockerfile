FROM postgres:12

ENV POSTGRES_DB=speechrecognition
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=postgres

COPY postgresql.conf /etc/postgresql/postgresql.conf
COPY pg_hba.conf /etc/postgresql/pg_hba.conf

CMD ["postgres", "-c", "config_file=/etc/postgresql/postgresql.conf", "-c", "hba_file=/etc/postgresql/pg_hba.conf"]

EXPOSE 5436