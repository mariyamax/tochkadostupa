package org.example;

import java.sql.*;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

//Запросы к базе
public class DataBaseConnection {

    //Пример урла для подключения - jdbc:mysql://192.168.45.226:400/basa
    //Если mysql, то 5, с 8 будут проблемы
    private static String url = "";
    private static String user = "login";
    private static String password = "password";
    private static final String QUERY_ERROR = "Problem with query ";
    private static final String START_CONNECTION_ERROR = "Can not connect with database";
    private static final String STOP_CONNECTION_ERROR = "Can not close database connection";
    private static Connection connection;

    private static Connection connect() {
        if (connection == null) {
            try {
                connection = DriverManager.getConnection(url, user, password);
            } catch (SQLException e) {
                System.out.println(e);
            } catch (Exception e) {
                throw new RuntimeException(e);
            }
        }
        return connection;
    }

    public static List<HashMap<String, String>> executeQuery(String query) {
        List<HashMap<String, String>> tableData = new ArrayList<>();
        try (PreparedStatement preparedStatement = DataBaseConnection.connect().prepareStatement(query)) {
            preparedStatement.execute();
            ResultSet set = preparedStatement.getResultSet();
            ResultSetMetaData metaData = set.getMetaData();
            int count = metaData.getColumnCount();
            String[] columnName = new String[count];
            for (int i = 1; i <= count; i++) {
                columnName[i - 1] = metaData.getColumnLabel(i);
            }
            while (set.next()) {
                int i = 0;
                HashMap<String, String> lineMap = new HashMap<>();
                while (i < columnName.length) {
                    lineMap.put(columnName[i], set.getObject(columnName[i]).toString());
                    i++;
                }
                tableData.add(lineMap);
            }
        } catch (SQLException e) {
            System.out.println(e);
        }
        return tableData;
    }

    public static int executeUpdate(String query) {
        try (PreparedStatement preparedStatement = DataBaseConnection.connect().prepareStatement(query)) {
            return preparedStatement.executeUpdate();
        } catch (SQLException e) {
            return -1;
        }
    }

    public static void closeConnection() {
        try {
            connection.close();
            connection = null;
        } catch (SQLException e) {
        }
    }

    public static ArrayList<Walls> select() {

        ArrayList<Walls> walls = new ArrayList<Walls>();
        try {
            PreparedStatement preparedStatement = DataBaseConnection.connect().prepareStatement("SELECT * FROM test.walls;");
            ResultSet resultSet = preparedStatement.executeQuery();
            while (resultSet.next()) {
                int x1 = resultSet.getInt(1);
                int y1 = resultSet.getInt(2);
                int x2 = resultSet.getInt(3);
                int y2 = resultSet.getInt(4);
                int k = resultSet.getInt(5);
                Walls wall = new Walls(x1, x2, y1, y2, k);
                walls.add(wall);
            }
        } catch (Exception ex) {
            System.out.println(ex);
        }
        return walls;
    }

}
