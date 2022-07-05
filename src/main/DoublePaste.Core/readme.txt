Everything below is copied from: https://www.alfredforum.com/topic/18061-accessing-clipboard-history-inside-a-workflow/#comment-93346

---

On a Workflow with multiple manual steps, you may find it useful to access recent clipboard history in order to pass along previously copied data to the next stage.

But if you switch context to the Clipboard History Viewer, you lose your place in the Workflow. To avoid that, you can load your history into a Script Filter and place it between the relevant steps of your Workflow. This post provides working code to do just that.

Things to note:

- This works by directly accessing Alfred’s clipboard history SQLite database, so it can be considered a hacky solution. Use at your own risk. That said, it only reads data and so far I haven’t bumped into problems.
- By default it shows the 40 most recent items from the clipboard history. Look for desc limit in the script to change that value.
- Only text entries are considered. Everything else is ignored.
- The code does not limit you to the clipboard contents. If you type something, that will be used instead.

Paste the code in a Script Filter without a Keyword, Argument Optional, and Language set to /usr/bin/ruby.

```ruby
require 'json'

ARGUMENT = ARGV[0]

unless ARGUMENT.nil?
  puts({ items: [{ title: ARGUMENT, arg: ARGUMENT }] }.to_json)
  exit 0
end

# Use clipboard when no argument given
require 'csv'
require 'open3'

CLIPBOARD_DB = File.join(ENV['HOME'], 'Library/Application Support/Alfred/Databases/clipboard.alfdb')

# The number after `desc limit` determines the number of results
CLIPBOARD_ITEMS = CSV.parse(Open3.capture2(
  '/usr/bin/sqlite3', '-csv', CLIPBOARD_DB,
  'select item from clipboard where dataType = 0 order by ts desc limit 40;'
).first).flatten

SCRIPT_FILTER_ITEMS = CLIPBOARD_ITEMS.each_with_object([]) { |item, array| array.push(title: item, arg: item) }

puts({ items: SCRIPT_FILTER_ITEMS }.to_json)
```
